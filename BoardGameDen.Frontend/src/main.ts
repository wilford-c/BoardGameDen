import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { AppComponent } from './app/app'; // Update to AppComponent

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient()] // Provide HttpClient directly
}).catch(err => console.error(err));