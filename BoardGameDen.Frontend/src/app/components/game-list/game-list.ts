import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { debounceTime, switchMap } from 'rxjs/operators';
import { GameService } from '../../services/game';
import { Game } from '../../models/game';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.html',
  styleUrls: ['./game-list.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class GameListComponent implements OnInit {
  games: Game[] = []; // Full list of games
  displayedGames: Game[] = []; // Paginated subset
  searchControl = new FormControl('');
  currentPage: number = 1;
  pageSize: number = 5; // 5 games per page
  totalPages: number = 1;
  searchTerm: string = ''; // Track search term for error message
  noResults: boolean = false; // Flag for "Game not found" message

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    console.log('GameListComponent initialized');
    this.loadGames();

    this.searchControl.valueChanges
      .pipe(
        debounceTime(300),
        switchMap(term => {
          this.searchTerm = term || '';
          console.log('Searching for:', term);
          this.currentPage = 1; // Reset to page 1 on new search
          return this.gameService.searchGames(term || '');
        })
      )
      .subscribe({
        next: (data: Game[]) => {
          console.log('Search results:', data);
          this.games = data;
          this.noResults = data.length === 0 && this.searchTerm !== '';
          this.updatePagination();
        },
        error: (error) => {
          console.error('Error searching games:', error);
          this.games = [];
          this.displayedGames = [];
          this.noResults = this.searchTerm !== '';
          this.totalPages = 1;
        }
      });
  }

  loadGames() {
    console.log('Fetching games...');
    this.gameService.getGames().subscribe({
      next: (data: Game[]) => {
        console.log('Games received:', data);
        this.games = data;
        this.noResults = false;
        this.updatePagination();
      },
      error: (error) => {
        console.error('Error fetching games:', error);
        this.games = [];
        this.displayedGames = [];
        this.noResults = false;
        this.totalPages = 1;
      }
    });
  }

  updatePagination() {
    this.totalPages = Math.ceil(this.games.length / this.pageSize);
    const startIndex = (this.currentPage - 1) * this.pageSize;
    this.displayedGames = this.games.slice(startIndex, startIndex + this.pageSize);
  }

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePagination();
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePagination();
    }
  }

  get visiblePageNumbers(): number[] {
    const maxVisiblePages = 5;
    const halfVisible = Math.floor(maxVisiblePages / 2);
    let startPage = Math.max(1, this.currentPage - halfVisible);
    let endPage = Math.min(this.totalPages, startPage + maxVisiblePages - 1);

    if (endPage - startPage < maxVisiblePages - 1) {
      startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    return Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i);
  }
}