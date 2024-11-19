"use strict";

// Class definition
var KTCandidateAssignGroupDetails = function () {
    // Shared variables
    const element = document.getElementById('assign_candidate_modal');
    const form = document.getElementById('assign_candidate_modal_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initCandidateGroupDetails = () => {

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
                    $('#assign_candidate_modal').off('hide.bs.modal');
                    modal.hide();
                    $('#assign_candidate_modal').on('hidden.bs.modal', function () {
                        assignCandidateModalCloseConfirmation();
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
                    $('#assign_candidate_modal').off('hide.bs.modal');
                    modal.hide();
                    $('#assign_candidate_modal').on('hidden.bs.modal', function () {
                        assignCandidateModalCloseConfirmation();
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
                        const remainingCandidates = [];
                        $('#assign_candidate_modal_scroll .candidate-item').each(function () {
                            remainingCandidates.push($(this).data('candidate-id'));
                        });

                        // Append remaining groups to form as hidden inputs
                        remainingCandidates.forEach(candidateId => {
                            const hiddenInput = $('<input>', {
                                type: 'hidden',
                                name: 'remainingCandidates[]',
                                value: candidateId
                            });
                            form.appendChild(hiddenInput[0]);
                        });

                        // Append candidateId to form as a hidden input
                        const groupId = form.dataset.groupId;
                        const hiddenInputGroupId = $('<input>', {
                            type: 'hidden',
                            name: 'groupId',
                            value: groupId
                        });
                        form.appendChild(hiddenInputGroupId[0]);

                        form.submit(); // Submit form
                    }
                });

            }, 2000);
        });
    }

    return {
        init: function () {
            initCandidateGroupDetails();
        }
    };
}();

async function getGroupForAssignCandidate(id) {
    try {
        const response = await $.ajax({
            url: `/candidateAdmin/candidategroup/assigncandidate/${id}`,
        });
        return response;
    } catch (error) {
        console.error('Error fetching candidate data:', error);
        throw error;
    }
}

// Tanımlanan event listener fonksiyonu
const excludedCandidateIds = new Set();
const candidateListArea = document.getElementById('assign_candidate_modal_scroll');
const candidateListSelect = document.getElementById('candidateListSelect');

//-------------------------------------
function updateWarning() {
    if (candidateListSelect.options.length === 0) {
        const option = document.createElement("option");
        option.id = 'nooption';
        option.value = "";
        option.disabled = true;
        option.selected = true;
        option.innerHTML = document.getElementById("localization_mesaage_no_candidate_available").value;
        candidateListSelect.appendChild(option);
        candidateListSelect.disabled = true;
        document.getElementById("addCandidateButton").disabled = true;

    } else {
        candidateListSelect.disabled = false;
        document.getElementById("addCandidateButton").disabled = false;
        const option = candidateListSelect.querySelector("#nooption");
        if (option) {
            candidateListSelect.removeChild(option);
        }
    }
}
//-------------------------------------

// Güncellenmiş çoklu ekleme yapan fonksiyon
function addCandidatesHandler() {
    const selectedOptions = Array.from(candidateListSelect.selectedOptions);
    selectedOptions.forEach(option => {
        addCandidateToList(option.value, option.text, candidateListArea, excludedCandidateIds);
        candidateListSelect.remove(option.index);
    });
    updateWarning();
}

// loadCandidateDataForAssignGroup fonksiyonu güncellenmesi
async function loadGroupDataForAssignCandidate(id) {
    const form = document.getElementById('assign_candidate_modal');
    try {
        candidateListArea.innerHTML = ''; // Clear any existing items
        candidateListSelect.innerHTML = ''; // Clear any existing options

        excludedCandidateIds.clear();
        const groupCandidates = await getGroupForAssignCandidate(id);
        const candidates = groupCandidates.candidates;
        const candidateList = groupCandidates.candidateList;
        const groupId = groupCandidates.groupId;

        const groupName = document.getElementById("GroupName");
        groupName.innerHTML = groupCandidates.name;

        // Store candidateId in form dataset
        const groupIdData = document.getElementById("groupId");
        groupIdData.value = groupId;

        // Collect IDs of already assigned groups and initially listed groups

        if (candidates.length === 0) {
            updateWarning();
        }

        // Populate combo box with groupList
        candidates.forEach(candidate => {
            addCandidateToList(candidate.id, candidate.fullName, candidateListArea, excludedCandidateIds);
        });
        candidateList.forEach(candidate => {
            if (!(excludedCandidateIds.has(candidate.id))) {
                const option = document.createElement('option');
                option.value = candidate.id;
                option.text = candidate.fullName;
                candidateListSelect.appendChild(option);
            }
        });

        // Eski event listener'ı kaldırıp yeni event listener'ı ekliyoruz
        document.getElementById('addCandidateButton').removeEventListener('click', addCandidatesHandler);
        document.getElementById('addCandidateButton').addEventListener('click', addCandidatesHandler);

        updateWarning();

    } catch (error) {
        console.error('Error loading candidate data into form:', error);
    }
}

function addCandidateToList(candidateId, candidateFullName, candidateListArea, excludedCandidateIds) {
    excludedCandidateIds.add(candidateId); // Add to the set of excluded IDs
    const candidateItem = document.createElement('div');
    candidateItem.classList.add('candidate-item', 'd-flex', 'justify-content-between', 'align-items-center', 'mb-2');
    candidateItem.dataset.candidateId = candidateId;
    candidateItem.innerHTML = `
        <span>${candidateFullName}</span>
        <button type="button" class="btn btn-danger btn-sm" onclick="removeCandidateItem(this)">&times;</button>
    `;
    candidateListArea.appendChild(candidateItem);
}

function removeCandidateItem(button) {
    const candidateListSelect = document.getElementById('candidateListSelect');

    const candidateItem = button.closest('.candidate-item');
    const candidateId = candidateItem.dataset.candidateId;
    const candidateFullName = candidateItem.querySelector('span').textContent;
    candidateItem.remove();

    // Re-add the removed group back to the combo box
    const option = document.createElement('option');
    option.value = candidateId;
    option.text = candidateFullName;
    candidateListSelect.appendChild(option);

    // Sort the combo box options alphabetically by text
    Array.from(candidateListSelect.options)
        .sort((a, b) => a.text.localeCompare(b.text))
        .forEach(option => candidateListSelect.appendChild(option));

    updateWarning();
}

//____________________________
function assignCandidateModalCloseConfirmation() {
    $('#assign_candidate_modal').on('hide.bs.modal', function (e) {
        e.preventDefault();
    });
}

assignCandidateModalCloseConfirmation();

$('#assign_candidate_modal').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
    //location.reload();
});

KTUtil.onDOMContentLoaded(function () {
    KTCandidateAssignGroupDetails.init();
});
