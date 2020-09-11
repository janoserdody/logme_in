export class ExchangeRatesDTO {
    date: Date;
    base: string;
    rates: { currency: string, rate: number; };
}

export class ExchangeRates {
    constructor(dto: ExchangeRatesDTO) {
        this.base = dto.base;
        this.date = dto.date;
        this.rates = Array.from(Object.keys(dto.rates).map(p => new Rate(p, dto.rates[p])));
    }

    date: Date;
    base: string;
    rates: Rate[];
}

export class Rate {
    constructor(currency: string, rate: number) {
        this.currency = currency;
        this.rate = rate;
    }

    currency: string;
    rate: number;
}
