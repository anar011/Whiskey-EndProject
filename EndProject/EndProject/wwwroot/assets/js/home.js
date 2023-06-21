$(document).ready(function () {

  "use strict"
  //main-slider//

  var swiper = new Swiper(".mySwiper", {
    slidesPerView: 1,
    spaceBetween: 30,
    keyboard: {
      enabled: true,
    },
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });


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

  let modal = document.querySelectorAll(".whiskey-icons .fa-square-plus")
  modal.forEach(element => {
    element.addEventListener("click", function (e) {
      e.preventDefault();
      document.querySelector(".main-product .product-priviews").classList.remove("d-none")
      document.querySelector("#overlay").style.display = "block"
      document.body.style.overflow = "hidden";

    })
  });


  let closee = document.querySelector(".whiskey-name span i")
  closee.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews").classList.add("d-none")
    document.querySelector("#overlay").style.display = "none"
    document.body.style.overflowY = "scroll"
  })

  // window.addEventListener("click", function (e) {
  //   if (e.target == document.querySelector(".main-product .product-priviews")) {
  //     document.querySelector(".main-product .product-priviews").classList.add("d-none")
  //     document.querySelector(".main-product").classList.add("d-none");
  //     document.querySelector("#overlay").style.display = "none";
  //     this.document.body.style.overflow = "unset"
  //   }
  // })



  //Modal price

  let oneprice = document.querySelector(".product-priviews .priviews-informs .whiskey-capacity").children[1]
  oneprice.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[0].classList.toggle("d-none")
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[1].classList.add("d-none")
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[2].classList.add("d-none")


  })





  let twoprice = document.querySelector(".product-priviews .priviews-informs .whiskey-capacity").children[2]
  twoprice.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[1].classList.toggle("d-none")
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[0].classList.add("d-none")
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[2].classList.add("d-none")
  })

  let threeprice = document.querySelector(".product-priviews .priviews-informs .whiskey-capacity").children[3]
  threeprice.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[2].classList.toggle("d-none")
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[0].classList.add("d-none")
    document.querySelector(".product-priviews .priviews-informs .whiskey-price").children[1].classList.add("d-none")
  })




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


















});