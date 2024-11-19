"use strict";

// Class definition
var KTCandidateAssignGroupDetails = function () {
    // Shared variables
    const element = document.getElementById('kt_modal_assign_group_user');
    const form = document.getElementById('kt_modal_assign_group_user_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initAssignGroupDetails = () => {

        // Close button handler
        const closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
        closeButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                title: localizedTexts.unsavedChangesTitle,
                text: localizedTexts.unsavedChangesText,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: localizedTexts.confirmButtonText,
                cancelButtonText: localizedTexts.cancelButtonText
            }).then((result) => {
                if (result.value) {
                    $('#kt_modal_assign_group_user').off('hide.bs.modal');
                    modal.hide();
                    $('#kt_modal_assign_group_user').on('hidden.bs.modal', function () {
                        updateModalCloseConfirmation();
                        $(this).find('form').trigger('reset');

                    });
                }
            });
        });

        // Cancel button handler
        const cancelButton = element.querySelector('[data-kt-users-modal-action="cancel"]');
        cancelButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                title: localizedTexts.unsavedChangesTitle,
                text: localizedTexts.unsavedChangesText,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: localizedTexts.confirmButtonText,
                cancelButtonText: localizedTexts.cancelButtonText
            }).then((result) => {
                if (result.value) {
                    $('#kt_modal_assign_group_user').off('hide.bs.modal');
                    modal.hide();
                    $('#kt_modal_assign_group_user').on('hidden.bs.modal', function () {
                        updateModalCloseConfirmation();
                        $(this).find('form').trigger('reset');


                    });
                }
            });
        });

        // Submit button handler
        const submitButton = element.querySelector('[data-kt-users-modal-action="submit"]');
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();

            submitButton.setAttribute('data-kt-indicator', 'on');
            submitButton.disabled = true;

            setTimeout(function () {
                submitButton.removeAttribute('data-kt-indicator');
                submitButton.disabled = false;

                Swal.fire({
                    text: localizedTexts.formSubmittedText,
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: localizedTexts.okButtonText,
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                }).then(function (result) {
                    if (result.isConfirmed) {
                        // Collect remaining groups
                        const remainingGroups = [];
                        $('#kt_modal_assign_group_user_scroll .group-item').each(function () {
                            remainingGroups.push($(this).data('group-id'));
                        });

                        // Append remaining groups to form as hidden inputs
                        remainingGroups.forEach(groupId => {
                            const hiddenInput = $('<input>', {
                                type: 'hidden',
                                name: 'remainingGroups[]',
                                value: groupId
                            });
                            form.appendChild(hiddenInput[0]);
                        });

                        // Append candidateId to form as a hidden input
                        const candidateId = form.dataset.candidateId;
                        const hiddenInputCandidateId = $('<input>', {
                            type: 'hidden',
                            name: 'candidateId',
                            value: candidateId
                        });
                        form.appendChild(hiddenInputCandidateId[0]);

                        form.submit(); // Submit form
                    }
                });

            }, 2000);
        });
    }

    return {
        init: function () {
            initAssignGroupDetails();
        }
    };
}();

async function getCandidateForAssignGroup(id) {
    try {
        const response = await $.ajax({
            url: `/candidateAdmin/candidate/assigngroup/${id}`,
        });
        return response;
    } catch (error) {
        console.error('Error fetching candidate data:', error);
        throw error;
    }
}


const excludedGroupIds = new Set();
const listArea = document.getElementById('kt_modal_assign_group_user_scroll');
const groupListSelect = document.getElementById('groupListSelect');

function updateWarning() {
    if (groupListSelect.options.length === 0) {
        const option = document.createElement("option");
        option.id = 'nooption';
        option.value = "";
        option.disabled = true;
        option.selected = true;
        option.innerHTML = document.getElementById("localization_mesaage_no_candidate_available").value;
        groupListSelect.appendChild(option);
        groupListSelect.disabled = true;
        document.getElementById("addGroupButton").disabled = true;

    } else {
        groupListSelect.disabled = false;
        document.getElementById("addGroupButton").disabled = false;
        const option = groupListSelect.querySelector("#nooption");
        if (option) {
            groupListSelect.removeChild(option);
        }
    }
}


// Tanımlanan event listener fonksiyonu
function addGroupsHandler() {
    const selectedOptions = Array.from(groupListSelect.selectedOptions);
    selectedOptions.forEach(option => {
        addGroupToList(option.value, option.text, listArea, excludedGroupIds);
        groupListSelect.remove(option.index);
    });
    updateWarning();
}

// loadCandidateDataForAssignGroup fonksiyonu güncellenmesi
async function loadCandidateDataForAssignGroup(id) {
    const form = document.getElementById('kt_modal_assign_group_user_form');
    try {
        listArea.innerHTML = ''; // Clear any existing items
        groupListSelect.innerHTML = ''; // Clear any existing options
        excludedGroupIds.clear();
        const candidateGroups = await getCandidateForAssignGroup(id);
        const groups = candidateGroups.groups;
        const groupList = candidateGroups.groupList;
        const candidateId = candidateGroups.candidateId;

        const candidateName = document.getElementById("CandidateName");
        candidateName.innerHTML = candidateGroups.fullName;

        // Store candidateId in form dataset
        const candidateIdData = document.getElementById("candidateId");
        candidateIdData.value = candidateId;

        // Collect IDs of already assigned groups and initially listed groups

        if (groups.length === 0) {
            updateWarning();
        }


        // Populate combo box with groupList
        groups.forEach(group => {
            addGroupToList(group.id, group.name, listArea, excludedGroupIds);
        });

        groupList.forEach(group => {
            if (!(excludedGroupIds.has(group.id))) {
                const option = document.createElement('option');
                option.value = group.id;
                option.text = group.name;
                groupListSelect.appendChild(option);
            }
        });

        // Eski event listener'ı kaldırıp yeni event listener'ı ekliyoruz
        document.getElementById('addGroupButton').removeEventListener('click', addGroupsHandler);
        document.getElementById('addGroupButton').addEventListener('click', addGroupsHandler);

        updateWarning();


    } catch (error) {
        console.error('Error loading candidate data into form:', error);
    }
}


function addGroupToList(groupId, groupName, listArea, excludedGroupIds) {
    excludedGroupIds.add(groupId); // Add to the set of excluded IDs
    const groupItem = document.createElement('div');
    groupItem.classList.add('group-item', 'd-flex', 'justify-content-between', 'align-items-center', 'mb-2', 'ms-2');
    groupItem.dataset.groupId = groupId;
    groupItem.innerHTML = `
        <span>${groupName}</span>
        <button type="button" class="btn btn-danger btn-sm" onclick="removeGroupItem(this)">&times;</button>
    `;
    listArea.appendChild(groupItem);

}

function removeGroupItem(button) {
    const groupItem = button.closest('.group-item');
    const groupId = groupItem.dataset.groupId;
    const groupName = groupItem.querySelector('span').textContent;
    groupItem.remove();

    // Re-add the removed group back to the combo box
    const option = document.createElement('option');
    option.value = groupId;
    option.text = groupName;
    groupListSelect.appendChild(option);

    // Sort the combo box options alphabetically by text
    Array.from(groupListSelect.options)
        .sort((a, b) => a.text.localeCompare(b.text))
        .forEach(option => groupListSelect.appendChild(option));

    updateWarning();
}

function assignGroupModalCloseConfirmation() {
    $('#kt_modal_assign_group_user').on('hide.bs.modal', function (e) {
        e.preventDefault();
    });
}

assignGroupModalCloseConfirmation();

$('#kt_modal_assign_group_user').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});

KTUtil.onDOMContentLoaded(function () {
    KTCandidateAssignGroupDetails.init();
});
