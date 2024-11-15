document.addEventListener("DOMContentLoaded", function () {
    const classField = document.getElementById("ClassId");
    const studentField = document.getElementById("StudentId");
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

        // Disable the student dropdown if no class is selected
        if (isNaN(selectedClassId)) {
            studentField.disabled = true;
            studentField.innerHTML = "<option value='' disabled='disabled' selected='selected'>Select a student</option>";
            return;
        }

        try {
            // Fetch students for the selected class
            const response = await fetch(`https://localhost:7125/students/list?id=${selectedClassId}`);
            const students = await response.json();

            // Populate the student dropdown
            studentField.innerHTML = "<option value='' disabled='disabled' selected='selected'>Select a student</option>";
            students.forEach(student => {
                const option = document.createElement("option");
                option.value = student.id;
                option.textContent = student.fullName; // Ensure `fullName` exists in your model
                studentField.appendChild(option);
            });

            // Enable the student dropdown
            studentField.disabled = false;
        } catch (error) {
            console.error("Error fetching students:", error);
            studentField.disabled = true;
            studentField.innerHTML = "<option value=''>Failed to load students</option>";
        }
    });
});
