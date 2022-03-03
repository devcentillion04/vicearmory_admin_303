import './about.css';

// import other section
import Newsletter from '../home/newsletter/newsletter';

function About() {
  return (
    <section className="about">
      <div className="container">
        <div className="row">
          <div className="gallery_title page_title col-12 text-center">
            <div className="page_title_box">
              <h2 className="text-uppercase">About US</h2>
              <p className="m-0">We carry everything from stripped receivers to complete rifles, and everything in between. Whether you’re starting a fresh build, or trying to round up those
                last accessories to perfect your tactical set up, Vice Armory can get you what you need. We offer top quality products from highly recognized brands such as 
                Aero Precision, Palmetto State Armory, Magpul, Geissele, CMC, and much more. Aside from attachments and performance enhancing components, we offer valuable 
                services such as laser engraving and cerakote, at a price that we promise you won’t beat. Work with our in-house designers and gunsmiths to create your 
                dream build, and make all your range buddies jealous. Stop in today and see how Vice Armory can help you!</p>
            </div>
          </div>
          <div className="meet_our_title gallery_title page_title col-12 text-center">
            <div className="page_title_box">
              <h2 className="text-uppercase">Meet our team</h2>
            </div>
          </div>
          <div className="about_meet_team">
            <section id="image_with_text_section" className="meet_image_with_text_section image_with_text_section">
              <div className="container">
                <div className="row align-items-center">
                  <div className="image_with_text_box image_with_text_box_2 animate__animated animate__faster col-xl-6 col-lg-12 col-md-12 col-sm-12 col-12">
                      <div className="image_box">
                        <img src="./Assets/text_image_1.jpg" alt="post_image_1" />
                      </div>
                  </div>
                  <div className="image_with_text_box image_with_text_box_1 animate__animated animate__faster col-xl-6 col-lg-12 col-md-12 col-sm-12 col-12">
                      <h4>Team 1</h4>
                      <p>We carry everything from stripped receivers to complete rifles, and everything in between. Whether you’re starting a fresh build, or trying to round up those
                        last accessories to perfect your tactical set up, Vice Armory can get you what you need. We offer top quality products from highly recognized brands such as 
                        Aero Precision, Palmetto State Armory, Magpul, Geissele, CMC, and much more. Aside from attachments and performance enhancing components, we offer valuable 
                        services such as laser engraving and cerakote, at a price that we promise you won’t beat. Work with our in-house designers and gunsmiths to create your 
                        dream build, and make all your range buddies jealous. Stop in today and see how Vice Armory can help you!</p>
                  </div>
                </div>
              </div>
            </section>
            <section id="image_with_text_section" className="image_with_text_section">
              <div className="container">
                <div className="row align-items-center">
                  <div className="image_with_text_box image_with_text_box_1 animate__animated animate__faster col-xl-6 col-lg-12 col-md-12 col-sm-12 col-12">
                      <h4>Team 1</h4>
                      <p>We carry everything from stripped receivers to complete rifles, and everything in between. Whether you’re starting a fresh build, or trying to round up those
                        last accessories to perfect your tactical set up, Vice Armory can get you what you need. We offer top quality products from highly recognized brands such as 
                        Aero Precision, Palmetto State Armory, Magpul, Geissele, CMC, and much more. Aside from attachments and performance enhancing components, we offer valuable 
                        services such as laser engraving and cerakote, at a price that we promise you won’t beat. Work with our in-house designers and gunsmiths to create your 
                        dream build, and make all your range buddies jealous. Stop in today and see how Vice Armory can help you!</p>
                  </div>
                  <div className="image_with_text_box image_with_text_box_2 animate__animated animate__faster col-xl-6 col-lg-12 col-md-12 col-sm-12 col-12">
                      <div className="image_box">
                        <img src="./Assets/text_image_1.jpg" alt="post_image_1" />
                      </div>
                  </div>
                </div>
              </div>
            </section>
          </div>
        </div>
      </div>
      <Newsletter />
    </section>
  );
}

export default About;
