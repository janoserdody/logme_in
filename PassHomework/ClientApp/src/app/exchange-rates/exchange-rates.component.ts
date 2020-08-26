import { Component, OnInit } from '@angular/core';
import { ExchangeRatesService } from '../services/exchange-rates.service';
import { ExchangeRates } from '../models/exchange-rates';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-exchange-rates',
  templateUrl: './exchange-rates.component.html',
  styleUrls: ['./exchange-rates.component.css']
})
export class ExchangeRatesComponent implements OnInit {

  exchangeRates: ExchangeRates = undefined;

  constructor(private exchangeRatesService: ExchangeRatesService) { }

  ngOnInit() {
    const subscription = this.exchangeRatesService.getLatestExchangeRates()
      .pipe(finalize(() => subscription.unsubscribe()))
      .subscribe((rates: ExchangeRates) => this.exchangeRates = rates);
  }

}
