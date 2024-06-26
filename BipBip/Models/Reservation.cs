﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public long TripId { get; set; }

        public int ReserveeId { get; set; }

        public DateTime ReservationDate { get; set; }

        public bool PaymentSituation { get; set; }

        public int NumberOfReservation { get; set; }

        public ReservationStatus Status { get; set; }

        public ServiceType Type { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public enum ReservationStatus
        {
            WaitingForConfirmation,
            Confirmed,
            Canceled,
            Completed
        }

        public enum ServiceType
        {
            Parcel,
            Normal
        }


        [Ignore]
        public Trip Trip { get; set; }
        [Ignore]
        public User Reservee { get; set; }
        [Ignore]
        public bool RatingVisible { get; set; }

        public Reservation()
        {
        }

    }
}

