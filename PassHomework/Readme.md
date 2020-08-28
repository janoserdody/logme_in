# Hello candidate!

This solution was created by the PASS team @ LogMeIn to measure your hands-on skills at ASP.NET Core and Angular.

It's a slightly modified version of the [Angular project template](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/angular?view=aspnetcore-3.1&tabs=visual-studio),
you can follow the link to find details on how to start or modify the app, but hopefully it's pretty straightforward.
Just make sure you have [npm](https://www.npmjs.com/get-npm) and the [.NET Core SDK](https://dotnet.microsoft.com/download) installed.

We also added the [Angular Material](https://material.angular.io/) npm package, Angular's popular set of UI components.

## Context

Imagine this app is used by your company's sales team to check the current day's currency exchange rates. It uses [a public API](https://exchangeratesapi.io/) to get 
the latest exchange rates using EUR as base, which are then displayed in simple, sortable, paginated table.

## Tasks
We prepared 2(+1) tasks that should not take up a lot of your time (max 3 hours), you will find them below.

### Getting started

You should fork this repository to your own account on GitHub. Start working in your own forked repository. Please separate the solution for each task either by putting all changes in a single commit or in separate commits in a single branch.

#### Task #1

Your first task is to make the base currency changeable. If a user clicks on a currency in the table, the rates should be loaded using the selected currency as base.

#### Task #2

Your second task is to store the response of the API in a memory cache to improve performance and reduce load on the exchange rate API. Do not worry about timezones, you can assume all of our users are in UTC 🙂.

#### Bonus task / bug hunt

There is a small memory leakage when requesting the exchange rates. Find it, and patch it.
