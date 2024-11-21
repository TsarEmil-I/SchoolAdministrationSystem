document.addEventListener("DOMContentLoaded", function () {
    const classField = document.getElementById("ClassId");
    const studentField = document.getElementById("StudentId");
    const sequenceField = document.getElementById("SequenceNumber");
    const leftAbsenceField = document.getElementById("LeftAbsenceDays");

    const option = document.createElement("option");
    option.innerHTML = "Select class";
    option.setAttribute("selected", "selected");
    option.setAttribute("disabled", "disabled");
    option.setAttribute("value", "0");
    classField.insertBefore(option, classField.firstChild);
    studentField.innerHTML = "<option value='' disabled='disabled' selected='selected'>Select a student</option>";

    classField.addEventListener("change", async function () {
        if (classField.value == 0) {
            return;
        }
        const selectedClassId = parseInt(classField.value, 10);

        if (isNaN(selectedClassId)) {
            studentField.disabled = true;
            studentField.innerHTML = "<option value='' disabled='disabled' selected='selected'>Select a student</option>";
            return;
        }

        try {
            const response = await fetch(`https://localhost:7125/students/list?id=${selectedClassId}`);
            const students = await response.json();

            studentField.innerHTML = "<option value='' disabled='disabled' selected='selected'>Select a student</option>";
            students.forEach(student => {
                const option = document.createElement("option");
                option.value = student.id;
                option.textContent = student.fullName;
                studentField.appendChild(option);
            });

            studentField.disabled = false;
        } catch (error) {
            console.error("Error fetching students:", error);
            studentField.disabled = true;
            studentField.innerHTML = "<option value=''>Failed to load students</option>";
        }
    });

});
