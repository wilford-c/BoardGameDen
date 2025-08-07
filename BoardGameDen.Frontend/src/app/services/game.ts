import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Game } from '../models/game';

interface ApiResponse {
  boardGames: Game[];
  totalCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = '/api/BoardGames'; // Use proxy path

  constructor(private http: HttpClient) {}

  getGames(): Observable<Game[]> {
    return this.http.get<ApiResponse>(this.apiUrl).pipe(
      map(response => response.boardGames), // Extract boardGames array
      catchError(error => {
        console.error('Error fetching games:', error);
        return of([]); // Return empty array on error
      })
    );
  }

  searchGames(term: string): Observable<Game[]> {
    return this.http.get<ApiResponse>(`${this.apiUrl}?search=${term}`).pipe(
      map(response => response.boardGames), // Extract boardGames array
      catchError(error => {
        console.error('Error searching games:', error);
        return of([]); // Return empty array on error
      })
    );
  }
}