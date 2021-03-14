// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let allcats = $('#allCategories');

let showingTrendsByDonations = $('.js-donations-trending');
showingTrendsByDonations.hide();

let donationsDropdown = $('.js-donations');
donationsDropdown.on('click', () => {
    allcats.hide();
    showingTrendsByDonations.show();
    categoryDrop.hide();
})

let trendsDrop = $('.js-donations');
trendsDrop.on('click', () => {
    showTrends.show();
})

let categoryDrop = $('.js-category-selection-btn');
categoryDrop.hide();

let basedOnCategories = $('.js-trendsByCategory-item');
basedOnCategories.on('click', () => {
    categoryDrop.show();
})

$('#CategoriesSelect').on('change', function () {
    allcats.show();
    categoryDrop.show();
    showingTrendsByDonations.hide();
    var e = document.getElementById("CategoriesSelect");
    var takeoption = e.options[e.selectedIndex].value;
    console.log(takeoption);

    if (takeoption == "Fashion") {
        $("#Fashion2").css("display", "block");
        $("#Film2").css("display", "none");
        $("#Games2").css("display", "none");
        $("#Journalism2").css("display", "none");
        $("#Music2").css("display", "none");
        $("#Publish2").css("display", "none");
        $("#Startup2").css("display", "none");
        $("#Technology2").css("display", "none");
    }
    else if (takeoption == "Film") {
        $("#Fashion2").css("display", "none");
        $("#Film2").css("display", "block");
        $("#Games2").css("display", "none");
        $("#Journalism2").css("display", "none");
        $("#Music2").css("display", "none");
        $("#Publish2").css("display", "none");
        $("#Startup2").css("display", "none");
        $("#Technology").css("display", "none");
    }
    else if (takeoption == "Games") {
        $("#Fashion2").css("display", "none");
        $("#Film2").css("display", "none");
        $("#Games2").css("display", "block");
        $("#Journalism2").css("display", "none");
        $("#Music2").css("display", "none");
        $("#Publish2").css("display", "none");
        $("#Startup2").css("display", "none");
        $("#Technology2").css("display", "none");
    }
    else if (takeoption == "Journalism") {
        $("#Fashion2").css("display", "none");
        $("#Film2").css("display", "none");
        $("#Games2").css("display", "none");
        $("#Journalism2").css("display", "block");
        $("#Music2").css("display", "none");
        $("#Publish2").css("display", "none");
        $("#Startup2").css("display", "none");
        $("#Technology2").css("display", "none");
    }
    else if (takeoption == "Music") {
        $("#Fashion2").css("display", "none");
        $("#Film2").css("display", "none");
        $("#Games2").css("display", "none");
        $("#Journalism2").css("display", "none");
        $("#Music2").css("display", "block");
        $("#Publish2").css("display", "none");
        $("#Startup2").css("display", "none");
        $("#Technology2").css("display", "none");
    }
    else if (takeoption == "Publish") {
        $("#Fashion2").css("display", "none");
        $("#Film2").css("display", "none");
        $("#Games2").css("display", "none");
        $("#Journalism2").css("display", "none");
        $("#Music2").css("display", "none");
        $("#Publish2").css("display", "block");
        $("#Startup2").css("display", "none");
        $("#Technology2").css("display", "none");
    }
    else if (takeoption == "Startup") {
        $("#Fashion2").css("display", "none");
        $("#Film2").css("display", "none");
        $("#Games2").css("display", "none");
        $("#Journalism2").css("display", "none");
        $("#Music2").css("display", "none");
        $("#Publish2").css("display", "none");
        $("#Startup2").css("display", "block");
        $("#Technology2").css("display", "none");
    }
    else if (takeoption == "Technology") {
        $("#Fashion2").css("display", "none");
        $("#Film2").css("display", "none");
        $("#Games2").css("display", "none");
        $("#Journalism2").css("display", "none");
        $("#Music2").css("display", "none");
        $("#Publish2").css("display", "none");
        $("#Startup2").css("display", "none");
        $("#Technology2").css("display", "block");
    }
})


let button = $('.donation-button')
button.on('click', (event) => {

    let CurrentMoneyButton = $(event.currentTarget);
    var project_id = $("#project_id").attr("data-Project-id");

    var donation_id = CurrentMoneyButton.attr("data-donating-id");
    console.log(project_id);

    console.log(donation_id);

    let dat = {
        ProjectId: project_id,
        DonationId: donation_id
    };

    console.log(dat);

    var datJson = JSON.stringify(dat);

    $.ajax({
        type: 'POST',
        url: '/KrowdSourced/Project/Donation',
        data: datJson,
        contentType: "application/json",
        success: function () {
            toastr.success('You have succesfully donated!');
        },
        error: function () {
            toastr.error('Hmm, an error occured. Brb...');
        }
    });
});

function copyToClipboard(element) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($(element).text()).select();
    document.execCommand("copy");
    $temp.remove();
};