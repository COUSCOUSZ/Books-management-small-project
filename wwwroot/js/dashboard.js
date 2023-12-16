document.addEventListener("DOMContentLoaded", () => {
  console.log("eeeeeeeeee");
  gsap.to(".loader", 0.01, { display: "block" });
  gsap.to(".loader_2", 0.01, { display: "block" });
  gsap.from(".loader .logo", 0.7, { x: 100, opacity: 0, delay: 0.5 });
  gsap.from(".loader .text", 0.75, { x: -100, opacity: 0, delay: 1 });
  gsap.to(".loader", 0.85, { height: 0, ease: "expo.out", delay: 1.6 });
  gsap.to(".loader_2", 0.85, { height: 0, ease: "expo.out", delay: 1.8 });
  gsap.to(".loader .img", 0.85, { y: 100, delay: 1.6 });
  // gsap.to(".loader .img",.85,{opacity:0,delay:1.6})

  // i want these animations below to start after the above finish

  let addDelay = 1.25;

  gsap.to(".loader", {
    onComplete: () => {
      console.log("ahahaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
      gsap.from(".dashboard_text h1", 0.75, {
        y: 100,
        opacity: 0,
        delay: addDelay,
        ease: "sine.out",
      });
      gsap.from(".dashboard_text p", 0.75, {
        y: 100,
        opacity: 0,
        delay: 0.25 + addDelay,
        ease: "sine.out",
      });
      gsap.from(".dashboard_text a", 0.75, {
        y: 100,
        opacity: 0,
        delay: 0.5 + addDelay,
        ease: "sine.out",
      });
      gsap.from(".dashboard_hero img", 0.75, {
        x: 25,
        opacity: 0,
        delay: 0.5 + addDelay,
        ease: "sine.out",
      });

      gsap.to(".dashboard_text a", 0.75, {
        boxShadow: "#d059ff66 8px 2px 20px 3px",
        delay: 1.5 + addDelay,
        ease: "sine.out",
      });

      gsap.from(".row_2_2", 0.75, {
        x: 25,
        opacity: 0,
        delay: 0.5 + addDelay,
        ease: "sine.out",
      });

      gsap.from(".row_2>*", 0.75, {
        opacity: 0,
        y: -50,
        delay: addDelay - 0.3,
      });

      gsap.from(".cards", 0.8, { opacity: 0, x: -50, delay: addDelay - 0.3 });

      // -----------------------------

      gsap.from(".heading-container", 0.8, {
        opacity: 0,
        x: 10,
        delay: addDelay - 0.3,
      });
      gsap.from(".controls", 0.8, {
        opacity: 0,
        x: 10,
        delay: 0.3,
        delay: addDelay - 0.3,
      });
      gsap.from(".table-container", 0.8, {
        opacity: 0,
        x: 10,
        delay: 0.2,
        delay: addDelay - 0.3,
      });

      document
        .querySelector(".dashboard_text a")
        .addEventListener("mouseover", () => {
          console.log("hovered");
          gsap.to(".dashboard_text a", 0.5, {
            boxShadow: "#c859ffba -2px -4px 20px 3px",
            ease: "sine.out",
          });
        });
      document
        .querySelector(".dashboard_text a")
        .addEventListener("mouseleave", () => {
          gsap.to(".dashboard_text a", 0.5, {
            boxShadow: "#d059ff66 8px 2px 20px 3px",
            ease: "sine.out",
          });
        });
    },
  });
});
