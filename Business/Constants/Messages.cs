using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants {
    public static class Messages {
        public static string BrandAdded = "Marka eklendi.";
        public static string BrandDeleted = "Marka silindi.";
        public static string BrandUpdated = "Marka güncellendi.";
        public static string CarAdded = "Araba eklendi.";
        public static string CarDeleted = "Araba silindi.";
        public static string CarUpdated = "Araba güncellendi.";
        public static string ColorAdded = "Renk eklendi.";
        public static string ColorDeleted = "Renk silindi.";
        public static string ColorUpdated = "Renk güncellendi.";
        public static string CustomerAdded = "Müşteri eklendi.";
        public static string CustomerDeleted = "Müşteri silindi.";
        public static string CustomerUpdated = "Müşteri güncellendi.";
        public static string RentalAdded = "Kiralama eklendi.";
        public static string RentalDeleted = "Kiralama silindi.";
        public static string RentalUpdated = "Kiralama güncellendi.";
        public static string CarAlreadyRentedInSpecifiedDate = "Seçtiğiniz araba belirttiğiniz tarihlerde halihazırda kiralanmış.";
        public static string RentalReturnDateIsBeforeRentDate = "İade tarihi, kiralama tarihinden önce olamaz.";
        public static string UserAdded = "Kullanıcı eklendi.";
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string UserUpdated = "Kullanıcı güncellendi.";
        public static string UserInfoUpdated = "Kullanıcı bilgileri güncellendi.";
        public static string PasswordDoesNotContainVarietyOfCharacters = "Şifre küçük harf, büyük harf ve rakam karakterlerinden en az birer tane içermelidir.";
        public static string CarImageAdded = "Araba resmi eklendi.";
        public static string CarImageDeleted = "Araba resmi silindi.";
        public static string CarImageUpdated = "Araba resmi güncellendi.";
        public static string CarHasMaxCountOfImages = $"Bir arabanın en fazla {Values.MaxCountOfImagesPerCar} resmi olabilir.";
        public static string NotFound = "Belirtilen içerik bulunamadı.";
        public static string AuthorizationDenied = "Bu işlem için yetkiniz yok.";
        public static string RegisterSuccessful = "Kullanıcı kayıt işlemi başarılı.";
        public static string LoginSuccessful = "Kullanıcı girişi başarılı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string WrongPassword = "Şifre hatalı.";
        public static string UserWithEmailAlreadyExists = "Bu e-posta adresiyle kayıtlı bir kullanıcı halihazırda mevcut. Lütfen farklı bir e-posta adresi girin.";
        public static string CustomerWithCompanyNameAlreadyExists = "Bu şirket ismiyle kayıtlı bir müşteri halihazırda mevcut. Lütfen farklı bir şirket ismi girin.";
        public static string AccessTokenCreated = "Erişim jetonu oluşturuldu.";
        public static string PaymentSuccessful = "Ödeme işlemi başarılı.";
        public static string PaymentFailed = "Ödeme işlemi başarısız oldu.";
        public static string PasswordChanged = "Şifreniz değiştirildi.";
        public static string CustomerFindexScoreIsInsufficient = "Maalesef findex puanınız bu aracı kiralamak için yetersiz. Lütfen daha düşük ücretli bir aracı kiralayın.";
        public static string CreditCardAdded = "Kredi kartı eklendi.";
        public static string CreditCardDeleted = "Kredi kartı silindi.";
        public static string CreditCardUpdated = "Kredi kartı güncellendi.";
    }
}
