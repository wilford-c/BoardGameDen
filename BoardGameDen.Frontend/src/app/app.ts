import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common'; // For directives if needed
import { GameListComponent } from './components/game-list/game-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, GameListComponent], // Include GameListComponent
  templateUrl: './app.html',
  styleUrls: ['./app.css'] // Use styleUrls for consistency
})
export class AppComponent { // Rename to AppComponent
  protected readonly title = signal('Board Games Den');
}