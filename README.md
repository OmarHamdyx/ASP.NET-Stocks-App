Stock Price Display Web Application Documentation
=================================================

Introduction
------------

This project aims to create an ASP.NET Core web application that displays live stock prices obtained from [Finnhub](https://finnhub.io/). The application architecture follows the n-layer (n-tier) design pattern for better maintainability and scalability.

Requirements
------------

*   ASP.NET Core Web Application
*   Integration with Finnhub API for live stock price updates
*   Use of n-layer architecture
*   Configuration management using `appsettings.json`
*   User-Secrets for storing API token securely
*   Creation of service interface `IFinnhubService` with methods to fetch company profile and stock price
*   Implementation of service methods to interact with Finnhub API
*   Creation of a view model class `StockTrade` to pass data from controller to view
*   Development of a view `Index.cshtml` to display stock details and update prices using WebSocket
*   Implementation of WebSocket connection to Finnhub for real-time price updates
*   Stock price refreshing for every one or two seconds during market hours

User Interface Design
---------------------

The main UI component is the `stocksapp/{symbol}` view, which displays live stock price updates. The view is designed to show stock details such as symbol, name, and current price.

Architecture
------------

The application follows the n-layer architecture for better separation of concerns and maintainability. Layers include presentation layer (controllers and views), service layer (business logic), and data access layer (API interaction).

Finnhub.io Integration
----------------------

[Finnhub](https://finnhub.io/) provides live stock price information online. The application integrates with Finnhub API to fetch stock details and live updates.

User-Secrets
------------

User-Secrets are utilized to securely store the Finnhub API token. Users can register on Finnhub to obtain their own token or use the provided token for making requests. The token needs to be stored in user-secrets and attached as part of the request URL while interacting with the Finnhub API.

appsettings
-----------

Configuration settings such as the default stock symbol (`DefaultSymbol`) are stored in `appsettings.json`.

FinnhubService
--------------

The `IFinnhubService` interface defines methods to fetch company profile and stock price. Implementation of this interface interacts with the Finnhub API to obtain the required data.

StockDetails View Model
---------------------

The `StockDetails` class is a view model used to pass stock data from the controller to the view. It includes properties such as `StockSymbol`, `StockName`and `Price`.

Index View
----------

The `stocksapp\GetStockDetails.cshtml` view displays stock details and updates prices using WebSocket. JavaScript code connects to the Finnhub WebSocket URL and updates prices on receiving messages.

Trade Controller
----------------

The `TradeController` includes action methods to handle requests and injects dependencies such as `IOptions` and `IFinnhubService`. The `GetStockDetails()` action method fetches stock details and price updates, creates a `StockDetails` object, and sends it to the view.

Instructions
------------

*   Create controllers with attribute routing.
*   Utilize Options pattern for configuration.
*   Inject `IOptions` in the controller.
*   Apply CSS styles and layout views as necessary.
*   Use provided CSS file for essential styles.
*   Inject `IFinnhubService` in the controller and invoke essential service methods.

Conclusion
----------

This documentation provides an overview of the Stock Price Display Web Application, detailing its architecture, integration with Finnhub API, UI design, and implementation instructions. The application aims to provide users with real-time stock price updates in a user-friendly interface.
