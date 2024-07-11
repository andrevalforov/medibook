$(document).ready(
  function () {
    $(".text-box, .text-area, .drop-down-list").each(function (index, control) {
      onControlChange($(control));
    });

    setTimeout(function () { $("body").removeClass("disable-animation"); }, 1000);
  }
);

$(document.body).on("change", ".text-box, .text-area, .drop-down-list", function () {
  var control = $(this);

  onControlChange(control);
});

function onControlChange(control) {
  if (control.val() == "") {
    control.removeClass("control--with-value");
  }

  else {
    control.addClass("control--with-value");
  }

  animateLabelFor(control);
}

$(document.body).on("focus", ".text-box, .text-area, .drop-down-list", function () {
  var control = $(this);

  control.addClass("control--focused");
  animateLabelFor(control);
});

$(document.body).on("blur", ".text-box, .text-area, .drop-down-list", function () {
  var control = $(this);

  control.removeClass("control--focused");
  animateLabelFor(control);
});

function animateLabelFor(control) {
  var label = control.parent().find(".label");

  if (control.hasClass("control--with-value") || ((control.hasClass("text-box") || control.hasClass("text-area")) && control.hasClass("control--focused"))) {
    label.addClass("field__label--with-value");
  }

  else {
    label.removeClass("field__label--with-value");
  }
}

$(document.body).on("click", ".tabs__tab", function () {
  $(".tabs__tab").removeClass("tabs__tab--active");
  $(".tab-pages__tab-page").hide();
  $(this).addClass("tabs__tab--active");
  $("#tabPage" + $(this).data("tabPageId")).show();
  return false;
});

$(".tabs__tab").first().click();

function getCulture() {
  if (location.href.indexOf("/ru/") != -1) {
    return "ru";
  }

  if (location.href.indexOf("/uk/") != -1) {
    return "uk";
  }

  return "en";
}

function onNavigationToggleClick() {
  var navigation = $("#navigation");

  if (navigation.is(":visible")) {
    navigation.slideUp("fast");
  }

  else {
    navigation.slideDown("fast");
  }
}

function onFilterChange() {
  var specializationId = $("#specializationId").val();
  var regionId = $("#regionId").val();
  var isOnline = $("#isOnline").val();

  location.href = "/doctors?specializationid=" + specializationId + "&regionid=" + regionId + "&isonline=" + isOnline;
}

function showUnableToCancelSupervisionMessage(hoursCount) {
  alert("На жаль, скасувати заплановану супервізію вже неможливо, оскільки до її початку залишилося менше " + hoursCount + " годин.");
}

function cancelConsultationByPatient(id) {
  if (!confirm("Скасувати консультацію?")) {
    return;
  }

  $.post("/patients/me/consultations/" + id + "/cancel", function () {
    location.reload();
  });
}

function cancelConsultationByDoctor(id) {
  if (!confirm("Скасувати консультацію?")) {
    return;
  }

  $.post("/doctors/me/consultations/" + id + "/cancel", function () {
    location.reload();
  });
}

function patientDidNotShowUp(id) {
  $.post("/consultations/" + id + "/mark-canceled", function () {
    location.reload();
  });
}

function bookConsultation(id) {
  var reason = $("#reason_" + id).val();
  var isOnline = $("input[name='isOnline_" + id + "']:checked").val();

  $.post("/consultations/" + id + "/book?reason=" + reason + "&isOnline=" + isOnline, function () {
    location.reload();
  });
}

function completeSupervisionBySupervisor(id, supervisorStatus, redirectUrl) {
  location.href = "/" + getCulture() + "/supervisors/me/supervisions/" + id + "/complete?supervisorstatus=" + supervisorStatus + "&redirectUrl=" + redirectUrl;
}

function completeSupervisionBySupervisee(id, superviseeStatus, redirectUrl) {
  location.href = "/" + getCulture() + "/supervisees/me/supervisions/" + id + "/complete?superviseestatus=" + superviseeStatus + "&redirectUrl=" + redirectUrl;
}

function onCalendarFiltersChange(role, id) {
  var year = $("#year").val();
  var month = $("#month").val();

  if (!year || !month)
    return;

  $.get(
    "/calendar/" + role + "/" + id + "?year=" + year + "&month=" + month,
    function (calendar) {
      $("#calendarContainer").html(calendar);
    }
  );
}


$(function () {
  var moreResponsesBtn$ = $('#showMoreResponses');

  if (!moreResponsesBtn$.length) return;

  moreResponsesBtn$.on('click', function () {
    $('#moreReviews').slideDown(300, function () { moreResponsesBtn$.hide(); });
  });
});

$(function () {
  function showMoreOnClick(moreResponsesBtn, dataContainer) {
    var moreResponsesBtn$ = $(moreResponsesBtn);
    if (!moreResponsesBtn$.length) return;

    moreResponsesBtn$.on('click', function (event) {
      var button = $(event.currentTarget);
      var request = new XMLHttpRequest();
      var path = location.pathname.substring(1, location.pathname.length);
      var culture = path.substring(0, path.indexOf('/'));
      var code = button.data('code');
      var apiType = button.data('type'); // supervisor or supervisee
      var skip = button.data('skip');
      var take = skip + 10;

      request.onreadystatechange = function (resp) {
        if (request.readyState == 2) {
          var total = request.getResponseHeader('Total');
          if (take > total) {
            button.remove();
          }
        }
        if (request.readyState == 4 && request.status == 200) {
          $(dataContainer).append(resp.currentTarget.response);
        }
      };

      request.open("GET", `/${culture}/api/${apiType}/supervisions?code=${code}&skip=${skip}&take=${take}`);
      request.send();

      button.data('skip', skip + 10);
    });
  }

  showMoreOnClick('#showMoreSupervisionsCurrent', '#supervisionsContainerCurrent');
  showMoreOnClick('#showMoreSupervisionsArchived', '#supervisionsContainerArchived');
});

function onTabClick(event) {
  const $tab = $(event.target).closest('.tabs__tab');

  if (!$tab.length) {
    return;
  }

  const $multitabWrapper = $tab.closest('#multitabWrapper');
  const tabPageId = $tab.data('tabPageId');

  if ($multitabWrapper.length) {
    $multitabWrapper.find('.tabs__tab').removeClass('tabs__tab--active');
    $multitabWrapper.find('.tab-pages__tab-page').hide();
  } else {
    const $tabs = $tab.parent('.tabs');
    $tabs.find('.tabs__tab').removeClass('tabs__tab--active');
    $(`.tab-pages[data-tabs-id="${$tabs.attr('id')}"] .tab-pages__tab-page`).hide();
  }

  if ($(window).width() < 1024) {
    $('#tabPageConsultation').hide();
  }

  const $indicator = $tab.find('.tabs__indicator');

  if ($indicator.length && parseInt($indicator.data('value')) > 0) {
    $indicator.data('value', 0);
    $indicator.text('');
  }

  if ($multitabWrapper.length) {
    const $tabPages = $multitabWrapper.find('.tabPage' + tabPageId);

    $multitabWrapper.find('.tabs__tab.' + tabPageId).addClass('tabs__tab--active');
    $tabPages.show();

    if (tabPageId === 'uk' || tabPageId === 'ru') {
      $tabPages.find('.form__field').attr('data-culture', tabPageId);
    }
  } else {
    const $tabPage = $('#tabPage' + tabPageId);

    $tab.addClass('tabs__tab--active');
    $tabPage.show();

    if (tabPageId === 'uk' || tabPageId === 'ru') {
      $tabPage.find('.form__field').attr('data-culture', tabPageId);
    }
  }

  if (tabPageId === 'Chat') {
    $('#consultChatBox').scrollTop($('#consultChatMessages').height());
  }

  return false;
}

$(function () {
  const $consultationTab = $('.tabs__tab[data-tab-page-id="Consultation"]');
  const $decktopConsultTabs = $('#consultTabs .tabs__tab:not(.tabs__tab--mobile)');
  const $tabPageConsultation = $('#tabPageConsultation');
  let isMobile = $(window).width() < 1024;

  $(document.body).on('click', '.tabs__tab', onTabClick);
  $(window).on('resize', switchConsultTabsOnResize);
  initActiveTab();

  function initActiveTab() {
    if ($consultationTab.length && $tabPageConsultation.length) {
      if (isMobile) {
        $consultationTab.click();
      } else {
        $decktopConsultTabs.first().click();
      }
    } else {
      $('.tabs__tab--active').first().click();
    }
  }

  function switchConsultTabsOnResize() {
    if ($(window).width() < 1024) {
      if (!isMobile) {
        isMobile = true;
        $consultationTab.click();
      }
    } else if (isMobile) {
      isMobile = false;
      if ($consultationTab.hasClass('tabs__tab--active')) {
        $decktopConsultTabs.first().click();
      } else {
        $tabPageConsultation.show();
      }
    }
  }
});