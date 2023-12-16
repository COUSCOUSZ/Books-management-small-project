const modal = document.querySelector(".modal");
const modalContent = document.querySelector(".modal_content");
const modalCancel = document.querySelector("#modal_cancel");
const modalAccept = document.querySelector("#modal_accept");
const modalDeleteTriggers = document.querySelectorAll(".trigger_delete_modal");
let currentLink;

modalDeleteTriggers.forEach((modalDeleteTrigger) => {
  modalDeleteTrigger.addEventListener("click", (e) => {
    currentLink = e.currentTarget.href;
    e.preventDefault();
    gsap.to(modal,.25,{opacity:1,display:"flex"})
    gsap.fromTo(modalContent,.25,{scale:0.95},{scale:1})
  });
});

modalCancel.addEventListener("click", () => {
    gsap.to(modalContent,.25,{scale:0.9})
    gsap.to(modal,.25,{opacity:0,display:"none"})
});

modalAccept.addEventListener("click", () => {
  window.location.href = currentLink;
});
