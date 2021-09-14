using API.Controllers;
using BLL.Services;
using BO.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using UnitTest.Services;
using Xunit;

namespace UnitTest
{
    //public class FoodControllerUnitTest:IFakeReservationService
    //{
    //    [Fact]
    //    public async void TestGetReservationById()
    //    {
    //        //arrange
    //        IReservationService reservationService = new FakeReservationService();
    //        ReservationController foodController = new ReservationController(reservationService);
    //        //End arrange

    //        //act
    //        OkObjectResult reservationActionResult = await foodController.GetReservationById(1) as OkObjectResult;
    //        NotFoundResult reservationActionResult1 = await foodController.GetReservationById(6) as NotFoundResult;
    //        //end act

    //        //Assert
    //        Assert.NotNull(reservationActionResult);
    //        Assert.NotNull(reservationActionResult1);
    //        Assert.Equal(200, reservationActionResult.StatusCode);
    //        //End Assert
    //        //todo

    //        var test = new Reservation()
    //        {
    //            Id = 1,
    //            Date =
    //            DateTime.Now,
    //            Nom = "test",
    //            Prenom = "test",
    //            Telephone = "tele",
    //            Midi_Soir = true,
    //            Quantite = 1,
    //            Formule = "test"
    //        };
    //        var test2 = new Reservation()
    //        {
    //            Id = 1,
    //            Date =
    //            DateTime.Now,
    //            Nom = null,
    //            Prenom = "test",
    //            Telephone = "tele",
    //            Midi_Soir = true,
    //            Quantite = 1,
    //            Formule = "test"
    //        };

    //        //act
    //        ActionResult<PageResponse<Reservation>> reservationListeAll = await foodController.GetAll(new PageRequest(){Page = 1, PageSize = 3 }) as ActionResult<PageResponse<Reservation>>;
    //        ActionResult<PageResponse<Reservation>> reservationListeAll2 = await foodController.GetAll(new PageRequest(){Page = 30, PageSize = 20 }) as ActionResult<PageResponse<Reservation>>;
    //        //end act
    //        //Assert
    //        Assert.NotNull(reservationListeAll);
    //        Assert.NotNull(reservationListeAll2);

    //        //End Assert



    //        //var obj1Str = JsonConvert.SerializeObject(reservationTest);
    //        //var obj2Str = JsonConvert.SerializeObject(reservationCreated);
    //        //Assert.Equal(obj1Str, obj2Str);
    //    }
    // }
}
