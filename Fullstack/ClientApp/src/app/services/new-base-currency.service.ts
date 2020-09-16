import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ExchangeRates, ExchangeRatesDTO } from "../models/exchange-rates";
import { map } from "rxjs/operators";

@Injectable()
export class NewBaseService {
  constructor(private http: HttpClient) { }

  getNewBase(currency: string): Observable<ExchangeRates> {
   if (currency == undefined){return}

   const paramValue = new HttpParams().append('currency', currency);
    return this.http.get<ExchangeRatesDTO>('newbase', {params: paramValue} ).pipe(map(dto => new ExchangeRates(dto)));
  }
}
