$(document).ready(function () {

    "use strict"


    //Home Search

    let search = document.querySelector(".header-search a i")
    search.addEventListener("click", function (e) {
        e.preventDefault()
        document.querySelector(".header-search input").classList.toggle("d-none")
    });


    //bodyde searchin yox olmasi

    // document.addEventListener("click", function (e) {
    //   if (!!!e.target.closest(".header-search a i")) {
    //       if (!$(".header-search input").classList.contains("d-none")) {
    //           $(".header-search input").classList.add("d-none")
    //       }
    //   }
    // })

    // if (document.body.classList.contains('thatClass')) {
    //   // do some stuff
    // }


    ///Modal




    //login-register

    let login = document.querySelector(".header-icons a .fa-user")
    login.addEventListener("click", function (e) {
        e.preventDefault()
        document.querySelector(".header-login-register").classList.toggle("d-none")
    });



    
    
  //tablet-navbar


  //bars
  let bars = document.querySelector(".tablet-navbar .tablet-navbar-menu-icon i")
  bars.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".tablet-navbar .tablet-navbar-menu").classList.toggle("d-none")
  });


  //search
  let searchTablet= document.querySelector(".tablet-navbar .tablet-navbar-icons").children[0]
  searchTablet.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".tablet-navbar-search").classList.toggle("d-none")
  });


  //login-register

  let register = document.querySelector(".tablet-navbar .tablet-navbar-icons").children[1]
  register.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".tablet-navbar .tablet-navbar-icons .header-login-register").classList.toggle("d-none")
  });


   //tablet-navbar




   //phone-navbar


   let phoneBars = document.querySelector(".phone-navbars .phone-navbar-icons i")
   phoneBars.addEventListener("click", function (e) {
     e.preventDefault()
     document.querySelector(".phone-navbars .phone-navbar-menu-icons").classList.toggle("d-none")
   });



     //search
  let searchPhone= document.querySelector(".phone-navbars .phone-navbar-menu-icons .phone-navbar-menu-icon-search i")
  searchPhone.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".phone-navbars .phone-navbar-menu-icons .tablet-navbar-search").classList.toggle("d-none")
  });



  // login-register

  let loginPhone= document.querySelector(".phone-navbars .phone-navbar-menu-icons .phone-navbar-menu-login-register").children[0]
  loginPhone.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".phone-navbars .phone-navbar-menu-icons .phone-login-register").classList.toggle("d-none")
  });






    //Basket-Cart







});
