(function (supervision) {
	supervision.ui = supervision.ui || {};
	supervision.ui.changedCommentStatus = function (id) {
		$.ajax({
			url: "/backend/supervisions/editpublishedstatus?supervisionid=" + id,
			type: "PATCH",
			success: () => window.location.reload(),
		});
	};

	supervision.ui.supervisionsListChanged = function () {
		location.href = platformus.url.combine(
				[
					{name: "supervisorid", value: $("#supervisor").val()},
					{name: "superviseeid", value: $("#supervisee").val()},
					{name: "status", value: $("#status").val()},
					{name: "isNotCompleted", value: $("#isNotCompleted").val()},
					{name: "scheduledFrom", value: $("#scheduledFrom").val()},
					{name: "scheduledTo", value: $("#scheduledTo").val()}
				]
		);
	};

	supervision.ui.certificatesListChanged = function () {
		location.href = platformus.url.combine(
				[
					{name: "superviseeid", value: $("#supervisee").val()},
					{name: "isSent", value: $("#isSent").val()},
					{name: "createdFrom", value: $("#createdFrom").val()},
					{name: "createdTo", value: $("#createdTo").val()},
					{name: "sentFrom", value: $("#sentFrom").val()},
					{name: "sentTo", value: $("#sentTo").val()}
				]
		);
	};

	supervision.ui.superviseesListChanged = function () {
		location.href = platformus.url.combine(
				[
					{name: "name", value: $("#name").val()},
					{name: "lastName", value: $("#lastName").val()},
					{name: "email", value: $("#email").val()}
				]
		);
	};

	supervision.ui.statisticsListChanged = function () {
		location.href = platformus.url.combine(
				[
					{name: "createdFrom", value: $("#createdFrom").val()},
					{name: "createdTo", value: $("#createdTo").val()}
				]
		);
	};

	function onCityListChange() {
		const organizationSelector = "organizationValue";
		const cityId = $('input[name="cityValue"]').val();
		const organizationItemsContainer = $(`#${organizationSelector} > .drop-down-list__items`);
		const culture = "uk";

		$.ajax({
			type: "GET",
			url: "/" + culture + "/api/organizations/" + cityId,
			success: response => {
				organizationItemsContainer.empty();
				createNewItems(response, organizationItemsContainer, organizationSelector);
				selectItem(response[0] || {}, organizationSelector);
				$(`input[name="${organizationSelector}"]`).trigger("change");
			}
		});
	}

	function onOrganizationListChange() {
		const positionSelector = "userPositionValue";
		const organizationId = $('input[name="organizationValue"]').val();
		const positionItemsContainer = $(`#${positionSelector} > .drop-down-list__items`);
		const culture = "uk";

		$.ajax({
			type: "GET",
			url: "/" + culture + "/api/userpositions/" + organizationId,
			success: response => {
				positionItemsContainer.empty();
				createNewItems(response, positionItemsContainer, positionSelector);
				selectItem(response[0] || {}, positionSelector);
			}
		});
	}

	function createNewItems(items, container, selector) {
		items.forEach(item => {
			const option = $('<a class="drop-down-list__item" href="#"></a>');

			option.text(item.text);
			option.attr("data-value", item.value);

			option.on('click', event => {
				const target = event.target;
				selectItem(
					{
						"text": target.text,
						"value": target.value,
					},
					selector,
				);
			});

			container.append(option);
		});
	}

	function selectItem(item, selector) {
		$(`#${selector} a.drop-down-list__item--selected`).text(item.text || "");
		$(`input[name="${selector}"]`).val(item.value || "");
	}

	function initializeSuperviseesHandlers() {
		$('input[name="cityValue"]').on('change', onCityListChange);
		$('input[name="organizationValue"]').on('change', onOrganizationListChange);
	}

	$(document).ready(() => {
		if ($('#superviseesForm').length) {
			initializeSuperviseesHandlers();
		}
	});

})(window.supervision = window.supervision || {});
