﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ParkingLib
{
    public class CardData : ICloneable
    {
        #region Structures

        private int _id = 0;

        [DisplayName("Id")]
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _code = string.Empty;

        [DisplayName("Code")]
        [JsonProperty("Code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _cardNumber = string.Empty;

        [DisplayName("CardNumber")]
        [JsonProperty("CardNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        private bool _isActive = true;

        [DisplayName("IsActive")]
        [JsonProperty("IsActive", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private string _cardType = string.Empty;

        [DisplayName("CardType")]
        [JsonProperty("CardType", NullValueHandling = NullValueHandling.Ignore)]
        public string CardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }

        private string _vehicleType = string.Empty;

        [DisplayName("VehicleType")]
        [JsonProperty("VehicleType", NullValueHandling = NullValueHandling.Ignore)]
        public string VehicleType
        {
            get { return _vehicleType; }
            set { _vehicleType = value; }
        }

        private int _balance = 0;

        [DisplayName("Balance")]
        [JsonProperty("Balance", NullValueHandling = NullValueHandling.Ignore)]
        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        private string _licensePlate = string.Empty;

        [DisplayName("LicensePlate")]
        [JsonProperty("LicensePlate", NullValueHandling = NullValueHandling.Ignore)]
        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; }
        }

        public CardData()
        {
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Data

        private SqlConnection _conn = Helper.OpenConnection();

        public List<CardData> Gets()
        {
            var dt = new DataTable();
            var ds = new DataSet();
            var lstCardData = new List<CardData>();
            var cardDataQuery =
                "SELECT card.Id,card.Code,card.CardNumber,card.IsActive,cardType.Name as CardType,vehicleType.Name as VehicleType,card.Balance,card.LicensePlate FROM dbo.Park_Card_Card card LEFT JOIN dbo.Park_Card_CardType cardType ON card.CardTypeId = cardType.Id LEFT JOIN dbo.Park_Vehicle_VehicleType vehicleType ON card.CardTypeId = vehicleType.Id";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (var da = new SqlDataAdapter(cardDataQuery, _conn))
            {
                using (new SqlCommandBuilder(da))
                {
                    da.Fill(ds, "CardData");
                    dt = ds.Tables["CardData"];
                }
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var cardData = new CardData();
                cardData.Id = Convert.ToInt32(dr["Id"]);
                cardData.Code = Convert.ToString(dr["Code"]);
                cardData.CardNumber = Convert.ToString(dr["CardNumber"]);
                cardData.IsActive = Convert.ToBoolean(dr["IsActive"]);
                cardData.CardType = Convert.ToString(dr["CardType"]);
                cardData.VehicleType = Convert.ToString(dr["VehicleType"]);
                cardData.Balance = Convert.ToInt32(dr["Balance"]);
                cardData.LicensePlate = Convert.ToString(dr["LicensePlate"]);

                lstCardData.Add(cardData);
            }

            _conn.Close();
            return lstCardData;
        }

        #endregion
    }
}