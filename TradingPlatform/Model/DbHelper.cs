using System;
using TradingPlatform.Data;

namespace TradingPlatform.Model
{
    public class DbHelper
    {
        private DataContext _context;
        public DbHelper(DataContext context)
        {
            _context = context;
        }

        public List<StockModel> GetStocks()
        {
            List<StockModel> response = new List<StockModel>();
            var dataList = _context.Stocks.ToList();
            dataList.ForEach(row => response.Add(new StockModel()
            {
                id = row.id,

                ticker = row.ticker,
                initial_price = row.initial_price,
                company_name = row.company_name,
                volume = row.volume,
                current_price = row.current_price,
                todays_max_price = row.todays_max_price,
                todays_min_price = row.todays_min_price,
                todays_open_price = row.todays_open_price

            }));
            return response;
        }

        public StockModel GetStockById(int id)
        {
            var row = _context.Stocks.Where(d => d.id.Equals(id)).FirstOrDefault();

            if (row == null)
            {
                return new StockModel();
            }

            return new StockModel()
            {
                id = row.id,
                ticker = row.ticker,
                initial_price = row.initial_price,
                company_name = row.company_name,
                volume = row.volume,
                current_price = row.current_price,
            };
        }

        public void SaveStock(StockModel stockModel)
        {
            Stock dbStock = new Stock();
            dbStock.id = stockModel.id;
            dbStock.ticker = stockModel.ticker;
            dbStock.initial_price = stockModel.initial_price;
            dbStock.company_name = stockModel.company_name;
            dbStock.volume = stockModel.volume;
            dbStock.current_price = stockModel.current_price;
            dbStock.todays_min_price = stockModel.current_price;
            dbStock.todays_max_price = stockModel.current_price;
            dbStock.todays_open_price = stockModel.current_price;

            //_context.Database.Log = Console.Write;
            _context.Stocks.Add(dbStock);
            _context.SaveChanges();
        }
        public List<UserInfoModel> GetUserInfo()
        {
            List<UserInfoModel> response = new List<UserInfoModel>();
            var dataList = _context.User_Infos.ToList();
            dataList.ForEach(row => response.Add(new UserInfoModel()
            {
                full_name = row.full_name,
                username = row.username,
                email = row.email,
                type = row.type,
                create_date = row.create_date,
                update_date = row.update_date,
                cash_balance = row.cash_balance
            }));
            return response;
        }

        public UserInfoModel GetUserInfoById(string currentUserID)
        {
            var row = _context.User_Infos.Where(d => d.id.Equals(currentUserID)).FirstOrDefault();

            if (row == null)
            {
                return new UserInfoModel();
            }

            return new UserInfoModel()
            {
                id = row.id,
                full_name = row.full_name,
                username = row.username,
                email = row.email,
                type = row.type,
                create_date = row.create_date,
                update_date = row.update_date,
                cash_balance = row.cash_balance
            };
        }

        public void UpdateUserInfo(UserInfoModel userInfoModel, string currentUserID)
        {
            Console.WriteLine("update userInfoModel : ", userInfoModel.username);
            Console.WriteLine("update userInfoModel : ", userInfoModel.full_name);

            var user = _context.User_Infos.Where(d => d.id.Equals(currentUserID)).FirstOrDefault();
            if (user == null) throw new Exception("");
            user.full_name = userInfoModel.full_name;
            user.username = userInfoModel.username;
            user.email = userInfoModel.email;
            user.type = userInfoModel.type;
            user.cash_balance = userInfoModel.cash_balance;
            user.update_date = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void SaveUserInfo(UserInfoModel userInfoModel, string currentUserID)
        {
            Console.WriteLine("save userInfoModel : ", userInfoModel.ToString());
            user_info dbUserInfo = new user_info();
            dbUserInfo.id = currentUserID;
            dbUserInfo.full_name = userInfoModel.full_name;
            dbUserInfo.username = userInfoModel.username;
            dbUserInfo.email = userInfoModel.email;
            dbUserInfo.type = userInfoModel.type;
            dbUserInfo.cash_balance = 500;
            dbUserInfo.update_date = DateTime.UtcNow;
            _context.User_Infos.Add(dbUserInfo);
            _context.SaveChanges();
        }

        public List<UserStockModel> GetUserStockById(string currentUserID)
        {
            List<UserStockModel> response = new List<UserStockModel>();
            {

                var dataList = (from us in _context.User_Stocks
                                join s in _context.Stocks
                                on us.stock_id equals s.id
                                where us.user_id == currentUserID
                                select new
                                {
                                    ticker = s.ticker,
                                    company_name = s.company_name,
                                    quantity = us.quantity,
                                    stock_id = us.stock_id,
                                    current_price = s.current_price

                                }).ToList();

                dataList.ForEach(row => response.Add(new UserStockModel()
                {

                    ticker = row.ticker,
                    company_name = row.company_name,
                    quantity = row.quantity,
                    stock_id = row.stock_id,
                    current_price = row.current_price,
                    current_amount = row.quantity * row.current_price

                }));

                return response;

            }
        }

        public List<UserOrderModel> GetUserOrderById(string currentUserID)
        {
            List<UserOrderModel> response = new List<UserOrderModel>();
            {

                var dataList = (from us in _context.User_Orders
                                join s in _context.Stocks
                                on us.stock_id equals s.id
                                where us.user_id == currentUserID
                                select new
                                {
                                    id = us.id,
                                    user_id = us.user_id,
                                    ticker = s.ticker,
                                    company_name = s.company_name,
                                    quantity = us.quantity,
                                    order_nature = us.order_nature,
                                    order_type = us.order_type,
                                    stock_id = us.stock_id,
                                    limit_price = us.limit_price,
                                    status = us.status,
                                    status_reason = us.status_reason,
                                    update_date = us.update_date

                                }).ToList();

                dataList.ForEach(row => response.Add(new UserOrderModel()
                {
                    id = row.id,
                    stock_id = row.stock_id,
                    ticker = row.ticker,
                    company_name = row.company_name,
                    order_nature = row.order_nature,
                    order_type = row.order_type,
                    quantity = row.quantity,
                    limit_price = row.limit_price,
                    status = row.status,
                    status_reason = row.status_reason,
                    update_date = row.update_date

                }));
                return response;
            }
        }

        public void SaveUserOrder(UserOrderModel userOrderModel, string currentUserID)
        {
            user_order dbUserOrder = new user_order();
            dbUserOrder.user_id = currentUserID;
            dbUserOrder.stock_id = userOrderModel.stock_id;
            dbUserOrder.quantity = userOrderModel.quantity;
            dbUserOrder.order_nature = userOrderModel.order_nature;
            dbUserOrder.order_type = userOrderModel.order_type;
            dbUserOrder.limit_price = userOrderModel.limit_price;
            dbUserOrder.status = "pending";
            dbUserOrder.status_reason = "Working on your order";
            if (userOrderModel.order_type == "limit")
            {
                DateTime today = DateTime.UtcNow;
                DateTime limitexpiration = today.AddDays(7);
                dbUserOrder.limit_expiration = limitexpiration;
            }

            _context.User_Orders.Add(dbUserOrder);
            _context.SaveChanges();
        }

        public void CancelUserOrder(CancelOrderModel userOrderModel, string currentUserID)
        {
            var userOrder = _context.User_Orders.Where(f => f.id == userOrderModel.order_id && f.user_id == currentUserID).FirstOrDefault();
            if (userOrder == null) throw new Exception("");
            userOrder.status = "canceled";
            userOrder.update_date = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public UserInfoModel GetUserCashBalanceById(string currentUserID)
        {
            var row = _context.User_Infos.Where(d => d.id.Equals(currentUserID)).FirstOrDefault();



            if (row == null)
            {
                return new UserInfoModel();
            }

            return new UserInfoModel()
            {

                cash_balance = row.cash_balance
            };
        }

        public void SaveUserCashBalance(UpdateCashBalanceRequest updateCashRequest, string currentUserID)
        {
            var user = _context.User_Infos.Where(f => f.id == currentUserID).FirstOrDefault();
            if (user == null) throw new Exception("");
            double newamount = updateCashRequest.amount + user.cash_balance;
            if (newamount < 0) throw new Exception("");
            user.cash_balance = newamount;
            user.update_date = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}





