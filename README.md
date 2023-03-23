# EasyTrading WebApplication

EasyTrading website is a stock trading platform, making trading super easy!


## Installation (easiest way)

1. Clone the repo

```bash
    git clone 
```

1. On the project's root folder, Run following to start UI, API and DB server.

Install docker, make sure the docker agent is running.

```bash
    docker-compose up --build
```

2. Run the following script that execute stored functions periodically and updates stock price and executes pending orders every 2secs.

```bash
    chmod +x ./Database/run_dbfuncs.sh
    ./Database/run_dbfuns.sh
```

3. EasyTrading should be serving at http://localhost:3001 (may wait for couple of mins if its not loaded yet).

This application is build for high resolution big monitors (preferred).

4. To become an admin user, please enter admin code : 500 in "Nickname" field of Cognito's Create Acount page. Traders can leave the "Nickname" field blank.

5. Happy trading!




# Following describes the 3 major components of this application in detail


# Trading DB Scripts

DB scripts to execute orders and generate random stock prices.

## Installation

1. Postgres Server - Store user and stock info
2. Pgadmin - GUI to show data


## Usage

1. Run the server
2. Create a database and run the scripts provided in `sql` directory
3. Execute following script to start running db function that executes transactions and generates random stock prices. 

```bash
./run_dbfuns.sh
```

## Future work

Stored procedure to not execute transactions during of hours

## License

[MIT](https://choosealicense.com/licenses/mit/)


# EasyTrading APIs (Backend)

Trade with EasyTrading by connecting to our easy to use APIs. 

## Get Started

Install .NET https://learn.microsoft.com/en-us/dotnet/core/install/

- Update appsettings.json file and update with database connetion string

```bash
dotnet run
```

## Usage

Swagger docs: http://localhost:8000/swagger/v1/swagger.json

## Future work

1. APIs to update market hours, holidays.
2. APIs to not execute any transaction during off hours.


## License

[MIT](https://choosealicense.com/licenses/mit/)


# EasyTrading UI (Frontend)

Trade with EasyTrading by our easy to use UI.

## Get Started

1. Learn about React https://react.dev/
2. Redux https://redux.js.org/
3. Typescript https://www.typescriptlang.org/
4. Amplify https://docs.amplify.aws/start/q/integration/react/

- The package containes aws-export-new file that has amplify connection details.
- Now run the following scripts

```bash
npm install
npm run start
```

## Usage

1. Create an account using /login page
2. Become an admin by putting 500 in Nickname field 

## Future work

1. Add a feature for Admin to update market hours
2. Disable placing orders during market hours
3. Drop down to pick stock
4. Add update date in charts and sort rows desc on date
5. Nickname to be replaced by custom attribute Passcode

## License

[MIT](https://choosealicense.com/licenses/mit/)


## Docker Future work
Trying to make docker compose work
