let state = "open";
const menuBtn = document.querySelector(".menu_button");
const lists = document.querySelectorAll(".sidenav-item");

// sidenav button clicked
menuBtn.addEventListener("click", () => {
  if (state === "open") {
    document.querySelector(".sidenav").style.width = "0";
    document.querySelector("#main").style.marginLeft = "0";
    state = "closed";
    localStorage.setItem("sidebar_state", "closed");
  } else if (state === "closed") {
    document.querySelector(".sidenav").style.width = "16rem";
    document.querySelector("#main").style.marginLeft = "16rem";
    state = "open";
    localStorage.setItem("sidebar_state", "opened");
  }
});

// sidenav & dropdown load

window.addEventListener("load", () => {
  if (localStorage.getItem("sidebar_state") == "closed") {
    document.querySelector(".sidenav").style.width = "0";
    document.querySelector("#main").style.marginLeft = "0";
    state = "closed";
  } else if (localStorage.getItem("sidebar_state") == "opened") {
    document.querySelector(".sidenav").style.width = "16rem";
    document.querySelector("#main").style.marginLeft = "16rem";
    state = "open";
  }

  lists.forEach(function (list) {
    if (localStorage.getItem(list.lastElementChild.innerHTML) == "opened") {
      list.classList.add("active-list");
    } else if (
      localStorage.getItem(list.lastElementChild.innerHTML) == "closed"
    ) {
      list.classList.remove("active-list");
    }
  });

  //color links
  let links = document.querySelectorAll(".sidenav-link a");
  links.forEach(function (link) {
    if (location.href == link) {
      link.parentElement.parentElement.parentElement.querySelector(
        ".sidenav-title"
      ).style.color = "#3c24ed";
      // link.parentElement.parentElement.parentElement.querySelector('.sidenav-link').style.backgroundColor = "#e6e6ea";
      link.style.color = "#3c24ed";
      link.parentElement.style.backgroundColor="#eeeefba6";
      // console.log(link.parentElement.parentElement.parentElement.querySelector('.sidenav-title'));
    }
  });

  let single_links = document.querySelectorAll(".sidenav-direct-link a");
  single_links.forEach(function (single_link) {
    if (location.href == single_link) {
      single_link.style.color = "#3c24ed";
      single_link.parentElement.querySelector("i").style.color = "#3c24ed";

      // console.log(link.parentElement.parentElement.parentElement.querySelector('.sidenav-title'));
    }
  });
});

//sidenav list :

lists.forEach(function (list) {
  const btn = list.querySelector(".sidenav-title");
  btn.addEventListener("click", () => {
    list.classList.toggle("active-list");

    if (list.classList.contains("active-list")) {
      localStorage.setItem(list.lastElementChild.innerHTML, "opened");
      // console.log("opened" + list);
    } else {
      localStorage.setItem(list.lastElementChild.innerHTML, "closed");
      // console.log("closed" + list);
    }
  });
});

// responsive :


  let screen = window.matchMedia("(max-width: 1100px)");
  let opsContainer = document.querySelector(".sidenav");
  if (screen.matches) {
    document.querySelector(".sidenav").style.width = "0";
    document.querySelector("#main").style.marginLeft = "0";
    state = "closed";
    localStorage.setItem("sidebar_state", "closed");
  }


window.addEventListener("resize", function (e) {
  if (screen.matches) {
    document.querySelector(".sidenav").style.width = "0";
    document.querySelector("#main").style.marginLeft = "0";
    state = "closed";
    localStorage.setItem("sidebar_state", "closed");
  } else {
    document.querySelector(".sidenav").style.width = "16rem";
    document.querySelector("#main").style.marginLeft = "16rem";
    state = "open";
    localStorage.setItem("sidebar_state", "opened");
  }
});

// --------------------------
