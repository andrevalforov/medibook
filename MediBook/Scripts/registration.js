
const cityList = document.getElementById('CityId');
if (cityList) {
  cityList.addEventListener('change', cityListChanged);
}

const organizationList = document.getElementById('OrganizationId');
if (organizationList) {
  organizationList.addEventListener('change', organisationListChanged);
}

function cityListChanged(e) {
  const id = e.currentTarget.value;
  const request = new XMLHttpRequest();
  const path = location.pathname.substring(1, location.pathname.length);
  const culture = path.substring(0, path.indexOf('/'));

  request.onreadystatechange = (resp) => {
    if (request.readyState == 4 && request.status == 200) {
      document.getElementById('OrganizationId').innerHTML = '';
      document.getElementById('UserPositionId').innerHTML = '';
      setNewOrganisations(JSON.parse(resp.currentTarget.response));
    }
  };

  request.open("GET", "/" + culture + "/api/organizations/" + id);
  request.send();
}

function setNewOrganisations(organisations) {
  organisations.forEach(org => {
    const option = document.createElement('option');
    option.textContent = org.text;
    option.value = org.value;

    document.getElementById('OrganizationId').append(option);
  });

  const event = document.createEvent('Event');
  event.initEvent('change', true, true);
  document.getElementById('OrganizationId').dispatchEvent(event);
}

function organisationListChanged(e) {
  const id = e.currentTarget.value;
  const request = new XMLHttpRequest();
  const path = location.pathname.substring(1, location.pathname.length);
  const culture = path.substring(0, path.indexOf('/'));

  request.onreadystatechange = (resp) => {
    if (request.readyState == 4 && request.status == 200) {
      document.getElementById('UserPositionId').innerHTML = '';
      setNewUserpositions(JSON.parse(resp.currentTarget.response));
    }
  };

  request.open("GET", "/" + culture + "/api/userpositions/" + id);
  request.send();
}

function setNewUserpositions(positions) {
  positions.forEach(org => {
    const option = document.createElement('option');
    option.textContent = org.text;
    option.value = org.value;

    document.getElementById('UserPositionId').append(option);
  });
}

function acceptLicenseChanged() {
  const button = $('#submitRegistration');

  button.attr('disabled')
   ? button.removeAttr('disabled')
   : button.attr('disabled', true);
}
