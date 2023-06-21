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
      document.querySelector(".product-priviews-detail").classList.remove("d-none")
      document.querySelector("#overlay").style.display = "block"
      document.body.style.overflow = "hidden";

    })
  });


  let closee = document.querySelector(".whiskey-name span i")
  closee.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews-detail").classList.add("d-none")
    document.querySelector("#overlay").style.display = "none"
    document.body.style.overflowY = "scroll"
  })


  //Modal price

  let oneprice = document.querySelector(".product-priviews-detail .priviews-informs .whiskey-capacity").children[1]
  oneprice.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews-detail .whiskey-price").children[0].classList.toggle("d-none")
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[1].classList.add("d-none")
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[2].classList.add("d-none")


  })





  let twoprice = document.querySelector(".product-priviews-detail .priviews-informs .whiskey-capacity").children[2]
  twoprice.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[1].classList.toggle("d-none")
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[0].classList.add("d-none")
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[2].classList.add("d-none")
  })

  let threeprice = document.querySelector(".product-priviews-detail .priviews-informs .whiskey-capacity").children[3]
  threeprice.addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[2].classList.toggle("d-none")
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[0].classList.add("d-none")
    document.querySelector(".product-priviews-detail .priviews-informs .whiskey-price").children[1].classList.add("d-none")
  })










  //login-register

  let login = document.querySelector(".header-icons a .fa-user")
  login.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".header-login-register").classList.toggle("d-none")
  });




  //prise


  class PriceRange extends HTMLElement {
    constructor() {
      super();
  
      console.log('Price Range: Constructor', this);
    }
  
    connectedCallback() {
      // Elements
      this.elements = {
        container: this.querySelector('div'),
        track: this.querySelector('div > div'),
        from: this.querySelector('input:first-of-type'),
        to: this.querySelector('input:last-of-type'),
        output: this.querySelector('output')
      }
  
      // Event listeners
      this.elements.from.addEventListener('input', this.handleInput.bind(this));
      this.elements.to.addEventListener('input', this.handleInput.bind(this));
  
      // Properties
      this.currency = (this.hasAttribute('currency') &&
                       this.getAttribute('currency') !== undefined &&
                       this.getAttribute('currency') !== '') ? this.getAttribute('currency') : '£';
            
      // Update the DOM
      this.updateDom();
  
      console.log('Price Range: Connected', this);
    }
  
    disconnectedCallback() {
      delete this.elements;
      delete this.currency;
  
      console.log('Price Range: Disconnected', this);
    }
    
    get from() {
      return parseInt(this.elements.from.value);
    }
    get to() {
      return parseInt(this.elements.to.value);
    }
    
    handleInput(event) {
      if (parseInt(this.elements.to.value) - parseInt(this.elements.from.value) <= 1) {
        if (event.target === this.elements.from) {
          this.elements.from.value = (parseInt(this.elements.to.value) - 1);
        } else if (event.target === this.elements.to) {
          this.elements.to.value = (parseInt(this.elements.from.value) + 1);
        }
      }
  
      // Update the DOM
      this.updateDom();
      
      console.log('Price Range: Updated!!', {
        from: parseInt(this.elements.from.value),
        to: parseInt(this.elements.to.value)
      });
    }
  
    updateDom() {
      this.drawFill();
      this.drawOutput();
    }
  
    drawFill() {
      const percent1 = (this.elements.from.value / this.elements.from.max) * 100,
            percent2 = (this.elements.to.value / this.elements.to.max) * 100;
  
      this.elements.track.style.background = `linear-gradient(to right, var(--track-color) ${percent1}%, var(--track-highlight-color) ${percent1}%, var(--track-highlight-color) ${percent2}%, var(--track-color) ${percent2}%)`;
    }
  
    drawOutput() {
      this.elements.output.textContent = `${this.currency}${this.elements.from.value} - ${this.currency}${this.elements.to.value}`;
    }
  }
  
  customElements.define('price-range', PriceRange);




  
  
  //tablet-navbar


  //bars
  let bars = document.querySelector(".tablet-navbar .tablet-navbar-menu-icon i")
  bars.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".tablet-navbar .tablet-navbar-menu").classList.toggle("d-none")
  });


  //search
  let searchTablet = document.querySelector(".tablet-navbar .tablet-navbar-icons").children[0]
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
  let searchPhone = document.querySelector(".phone-navbars .phone-navbar-menu-icons .phone-navbar-menu-icon-search i")
  searchPhone.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".phone-navbars .phone-navbar-menu-icons .tablet-navbar-search").classList.toggle("d-none")
  });



  // login-register

  let loginPhone = document.querySelector(".phone-navbars .phone-navbar-menu-icons .phone-navbar-menu-login-register").children[0]
  loginPhone.addEventListener("click", function (e) {
    e.preventDefault()
    document.querySelector(".phone-navbars .phone-navbar-menu-icons .phone-login-register").classList.toggle("d-none")
  });




});