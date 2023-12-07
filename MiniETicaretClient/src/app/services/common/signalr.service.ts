import { Inject, Injectable } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  constructor(@Inject("baseSignalRUrl") private baseSignalRUrl: string) {}
  private _connection: HubConnection;
  get connection(): HubConnection {
    return this._connection;
  }
  start(hubUrl: string) {
    if (
      !this.connection ||
      this._connection?.state == HubConnectionState.Disconnected
    ) {
      const builder: HubConnectionBuilder = new HubConnectionBuilder();
      const hubConnection: HubConnection = builder
        .withUrl(this.baseSignalRUrl+hubUrl)
        .withAutomaticReconnect()
        .build();

      hubConnection
        .start()
        .then(() => {
          console.log('connected');
        })
        .catch((error) => setTimeout(() => this.start(hubUrl), 2000));
      this._connection = hubConnection;
    }

    this._connection.onreconnected((connectionId) =>
      console.log('reconnected', connectionId)
    );

    this._connection.onreconnecting((error) =>
      console.log('reconnecting', error)
    );

    this._connection.onclose((error) =>
      console.log('close reconnection', error)
    );
  }

  invoke(
    procedureName: string,
    message: any,
    successCallBack?: (value) => void,
    errorCallBack?: (error) => void
  ) {
    this.connection
      .invoke(procedureName, message)
      .then(successCallBack)
      .catch(errorCallBack);
  }

  on(procedureName: string, callBack: (...message: any) => void) {
    this.connection.on(procedureName, callBack);
  }
}
