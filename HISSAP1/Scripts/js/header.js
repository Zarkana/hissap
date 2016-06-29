//Used to show/hide the drop down select site menu
$(document).ready(function () {
  $(".change-site a").click(function (e) {
    e.preventDefault();//Disable link
    $(this).toggleClass("selected");//Add selected class to the link
    $(".select-site").toggle();//Show or hide the dropdown
  });
});
