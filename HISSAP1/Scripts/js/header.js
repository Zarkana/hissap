//Used to show/hide the drop down select site menu
$(document).ready(function () {
  $(".change-site a").click(function (e) {
    e.preventDefault();//Disable link
    $(this).toggleClass("selected");//Add selected class to the link
    $(".select-site").toggle();//Show or hide the dropdown
  });
});

$(document).ready(function () {
  FillContract();
  FillSite();
});

function FillSite() {
  var contractId = $('#SelectedContract').val();
  var siteId = $('#SelectedSite').val();
  $.ajax({
    url: '/CurrentSites/FillSite',
    type: "GET",
    dataType: "JSON",
    data: { Contract: contractId },
    success: function (sites) {
      $("#SelectedSite").html(""); // clear before appending new list
      $.each(sites, function (i, site) {
        if (siteId == site.Id) { // test to see if option needs to be selected
          $("#SelectedSite").append(
            $('<option selected="selected"></option>').val(site.Id).html(site.SiteName));
        } else {
          $("#SelectedSite").append(
              $('<option></option>').val(site.Id).html(site.SiteName));
        }
      });
    }
  });
}

function FillContract() {
  var providerId = $('#SelectedProvider').val();
  var contractId = $('#SelectedContract').val();
  $.ajax({
    url: '/CurrentSites/FillContract',
    type: "GET",
    dataType: "JSON",
    data: { Provider: providerId },
    success: function (contracts) {
      $("#SelectedContract").html(""); // clear before appending new list
      $.each(contracts, function (i, contract) {
        if (contractId == contract.Id) { // test to see if option needs to be selected
          $("#SelectedContract").append(
            $('<option selected="selected"></option>').val(contract.Id).html(contract.ContractName));
        } else {
          $("#SelectedContract").append(
              $('<option></option>').val(contract.Id).html(contract.ContractName));
        }
      });
      FillSite();
    }
  });  
}

function SetProvider() {
}




//function FillCity() {
//  var stateId = $('#State').val();
//  $.ajax({
//    url: '/Employees/FillCity',
//    type: "GET",
//    dataType: "JSON",
//    data: { State: stateId },
//    success: function (cities) {
//      $("#City").html(""); // clear before appending new list
//      $.each(cities, function (i, city) {
//        $("#City").append(
//            $('<option></option>').val(city.CityId).html(city.CityName));
//      });
//    }
//  });
//}