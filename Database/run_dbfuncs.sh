#!/bin/sh
export PGPASSWORD=postgres

while :
do
	psql -U postgres -w -d postgres -h localhost -p 5435 -c "select * from public.execute_market_limit_buy()"
	echo "buy orders processed..."
	sleep 2
	psql -U postgres -w -d postgres -h localhost -p 5435 -c "select * from public.execute_market_limit_sell()"
	echo "sell orders processed..."
	sleep 2
	psql -U postgres -w -d postgres -h localhost -p 5435 -c "select * from public.update_marketstock_price()"
	echo "stock price updated..."
	sleep 2
done
