import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';
import { HttpClient } from '@angular/common/http';

import { Message } from 'primeng/api';
import {PlayerJournalModel} from '../models/PlayerJournalModel';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  private _hubConnection: HubConnection;
  private playerJournalModel : PlayerJournalModel ;
  private playerJournalModels : PlayerJournalModel[] ;
  msgs: Message[] = [];

  constructor(private http : HttpClient) { }

  ngOnInit(): void {
    //TODO: Call API to get all data
    this.http.get('http://localhost:1874/api/Message/gets/1').subscribe((data: PlayerJournalModel[]) => {
      this.playerJournalModels = data;
      console.log("data after database changes");
      console.log(data);
    });
    this._hubConnection = new HubConnection('http://localhost:1874/notify');
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('BroadcastMessage', (response: PlayerJournalModel[]) => {
      //TODO: only update those models which values are changed
      this.playerJournalModel = response[0];
      //ToDo: update this model, if not exist then add this new model.
       var objIndex = this.playerJournalModels.findIndex((model => model.journalItemId == this.playerJournalModel.journalItemId));
       this.playerJournalModels[objIndex] = this.playerJournalModel;
    console.log("response after database changes");
      console.log(response);
    });
  }
  ConvertString(value){
    return parseInt(value)
    }
}
