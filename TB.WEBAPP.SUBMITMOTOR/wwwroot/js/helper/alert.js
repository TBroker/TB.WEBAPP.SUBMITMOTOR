// แสดงกล่องยืนยันแบบมีปุ่ม Confirm / Deny
export async function showConfirmationDialog(options) {
    const result = await Swal.fire({
        icon: options.icon,
        title: options.title,
        html: options.text,
        showDenyButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: options.confirmButtonText,
        denyButtonText: options.denyButtonText,
    });
    return result;
}

// แสดงกล่องยืนยันแบบกำหนดค่าปรับแต่งเพิ่มเติม
export async function showCustomConfirmationDialog(options) {
    const result = await Swal.fire({
        icon: options.icon,
        title: options.title,
        html: options.text,
        showDenyButton: options.showDenyButton,
        denyButtonText: options.denyButtonText,
        confirmButtonColor: "#3085d6",
        confirmButtonText: options.confirmButtonText,
        allowOutsideClick: false,
        footer: options.footer,
    });
    return result;
}

// แสดงข้อความแจ้งเตือนแบบไม่มีปุ่ม
export async function showAlert(options) {
    await Swal.fire({
        position: "center",
        icon: options.icon,
        title: options.title,
        html: options.text,
        showConfirmButton: false,
    });
}

// แสดงข้อความแจ้งเตือนและเลื่อน scroll ไปยัง input ที่ error
export async function showAlertAndFocus(options) {
    await Swal.fire({
        position: "center",
        icon: options.icon,
        title: options.title,
        html: options.text,
        showConfirmButton: false,
        didClose: () => {
            const firstInvalidInput = options.object.first();
            $("html, body").animate(
                {
                    scrollTop: firstInvalidInput.offset().top + 40,
                },
                100,
                () => {
                    firstInvalidInput.focus();
                }
            );
        },
    });
}

// แสดงข้อความแจ้งเตือน และ redirect ไปยัง URL เมื่อกด Confirm
export async function showAlertAndRedirect(options) {
    const result = await Swal.fire({
        title: options.title,
        html: options.html,
        icon: options.icon,
        allowOutsideClick: false,
        confirmButtonText: options.confirmButtonText,
    });

    if (result.isConfirmed) {
        window.location = options.url;
    }

    return result;
}