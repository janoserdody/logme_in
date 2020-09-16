import { Component, OnInit, ViewChild } from '@angular/core';
import { ExchangeRatesService } from '../services/exchange-rates.service';
import { ExchangeRates, Rate } from '../models/exchange-rates';
import { finalize } from 'rxjs/operators';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { NewBaseService } from '../services/new-base-currency.service';

@Component({
  selector: 'app-exchange-rates',
  templateUrl: './exchange-rates.component.html',
  styleUrls: ['./exchange-rates.component.css']
})
export class ExchangeRatesComponent implements OnInit {

  exchangeRates: ExchangeRates = undefined;
  tableDataSource: MatTableDataSource<Rate> = undefined;
  currency: string = undefined;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(private exchangeRatesService: ExchangeRatesService, private newBaseService: NewBaseService) { }

  ngOnInit() {
    const subscription = this.exchangeRatesService.getLatestExchangeRates()
      .pipe(finalize(() => subscription.unsubscribe()))
      .subscribe((rates: ExchangeRates) => {
        this.exchangeRates = rates;
        this.tableDataSource = new MatTableDataSource(rates.rates);
        this.tableDataSource.sort = this.sort;
        this.tableDataSource.paginator = this.paginator;
      });
  }

  getNewBase(currency:string){
    this.currency = currency;

    const subscriptionBase = this.newBaseService.getNewBase(this.currency)
      .pipe(finalize(() => subscriptionBase.unsubscribe()))
      .subscribe((rates2: ExchangeRates) => {
        this.exchangeRates = rates2;
        this.tableDataSource = new MatTableDataSource(rates2.rates);
        this.tableDataSource.sort = this.sort;
        this.tableDataSource.paginator = this.paginator;
      });
  }
}
