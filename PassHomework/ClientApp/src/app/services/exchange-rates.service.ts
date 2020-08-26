import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ExchangeRates } from "../models/exchange-rates";

@Injectable()
export class ExchangeRatesService {
  constructor(private http: HttpClient) { }

  getLatestExchangeRates(): Observable<ExchangeRates> {
    return this.http.get<ExchangeRates>('exchangerates');
  }
}
