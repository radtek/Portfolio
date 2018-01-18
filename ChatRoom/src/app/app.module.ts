import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { ChatService } from './chat.service';
import { ChatComponent } from './chat/chat.component';

const appRoutes: Routes = [
  { path: 'chats', component: ChatComponent },
  // otherwise redirect to home
  { path: '**', redirectTo: 'chats'}
];

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [ChatService],
  bootstrap: [AppComponent]
})
export class AppModule { }
