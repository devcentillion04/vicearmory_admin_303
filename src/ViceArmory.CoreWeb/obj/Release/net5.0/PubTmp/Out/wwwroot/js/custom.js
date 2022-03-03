// Loader animation
$(window).on("load", function () {
  $("#win_loader").addClass('loader_hide');
  setTimeout(function () {
    $(".loader").addClass('loader_none');
  }, 1000);
});

$(document).ready(function () {
  $('.navbar-toggler').click(function () {
    $('#collapsibleNavbar').addClass('show');
  });
  $('.close_btn ').click(function () {
    $('#collapsibleNavbar').removeClass('show');
  });

  window.onscroll = function () {
    myFunction()
  };

  var navbar = document.getElementById("header");
  var sticky = navbar.offsetTop;

  function myFunction() {
    if (window.pageYOffset >= sticky) {
      navbar.classList.add("sticky")
    } else {
      navbar.classList.remove("sticky");
    }
  }

  $('.owl-carousel').owlCarousel({
    nav: true,
    items: 1,
    dots: false,
  });
});

// animate js
var HDMaster = {

  // Featured Animation 
  featured_animate: function () {
    var sticky = document.getElementById("featured_section");
    var sticky = sticky.offsetTop - 500;
    if (window.matchMedia('(min-height: 760px)').matches) {
      $('.featured_post').addClass("animate__slideInUp");
    } else {
      $(window).on('scroll', function () {
        //console.log(sticky);
        if (window.pageYOffset >= sticky) {
          $('.featured_post').addClass("animate__slideInUp");
        }
      });
    }
  },

  // Gallery Animation 
  gallery_animate: function () {
    var sticky = document.getElementById("gallery_section");
    var sticky = sticky.offsetTop - 800;
    $(window).on('scroll', function () {
      //console.log(sticky);
      if (window.pageYOffset >= sticky) {
        $('.gallery_section').addClass("animate__slideInUp");
        setTimeout(function () {
          $('.gallery_section .gallery_post .post_box').addClass("animate__slideInUp");
        }, 1000);
      }
    });
  },


  // Gallery Animation 
  image_with_text_animate: function () {
    var sticky = document.getElementById("image_with_text_section");
    var sticky = sticky.offsetTop - 600;
    $(window).on('scroll', function () {
      //console.log(sticky);
      if (window.pageYOffset >= sticky) {
        $('.image_with_text_section .image_with_text_box_1').addClass("animate__slideInLeft");
        $('.image_with_text_section .image_with_text_box_2').addClass("animate__slideInRight");
      }
    });
  },

  // Newsletter Animation 
  newsletter_animate: function () {
    var sticky = document.getElementById("newsletter_section");
    var sticky = sticky.offsetTop - 800;
    $(window).on('scroll', function () {
      //console.log(sticky);
      if (window.pageYOffset >= sticky) {
        $('.newsletter_section').addClass("animate__slideInUp");
        $('.footer').addClass("animate__fadeIn");
      }
    });
  },
  
  // Contact Newsletter Animation 
  newsletter_animate_contact: function () {
    var sticky = document.getElementById("newsletter_section");
    var sticky = sticky.offsetTop - 1500;
//    $(window).on('scroll', function () {
      //console.log(sticky);
      if (window.pageYOffset >= sticky) {
        $('.newsletter_section').addClass("animate__slideInUp");
        $('.footer').addClass("animate__fadeIn");
      }
//    });
  },

}

// Gallery Page
var galeerypage = {
  gallery_popup: function () {
    $('.popup-gallery').magnificPopup({
      delegate: 'a',
      type: 'image',
      tLoading: 'Loading image #%curr%...',
      mainClass: 'mfp-img-mobile',
      gallery: {
        enabled: true,
        navigateByImgClick: true,
        preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
      },
      image: {
        tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
      }
    });
  }
}


$(document).ready(function () {
  if ($('.sticky_header').hasClass('homepage')) {
    HDMaster.featured_animate();
    HDMaster.gallery_animate();
    HDMaster.image_with_text_animate();
    HDMaster.newsletter_animate();
  } else if ($('.sticky_header').hasClass('gallerypage')) {
    HDMaster.gallery_animate();
    HDMaster.newsletter_animate();
    galeerypage.gallery_popup();
  } else if ($('.sticky_header').hasClass('aboutpage')) {
    HDMaster.image_with_text_animate();
    HDMaster.newsletter_animate();
  } else if ($('.sticky_header').hasClass('weeklypage')) {
    HDMaster.featured_animate();
    HDMaster.newsletter_animate();
  } else if ($('.sticky_header').hasClass('contactpage')) {
    HDMaster.newsletter_animate_contact();
  } else if ($('.sticky_header').hasClass('loginpage')) {
    HDMaster.newsletter_animate_contact();
  }
});
