import * as Alerts from "../../helper/alert.js";

export async function initSubmitConfrim() {
    document.getElementById('verifyCardIdButton').addEventListener('click', async function () {
        const button = this;
        const form = document.getElementById('verifyEmailForm');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังตรวจสอบ...';

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        fetch('/VerifyIdentity/ConfirmVerifyCardID', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(async data => {
                if (data.success) {
                   await Alerts.showAlert(new Object({
                        icon: `success`,
                        title: `<h5>แจ้งเตือน</h4>`,
                        text: `<span class="text-success">${data.message}</div>`,
                    }));
                    return;
                }

                await Alerts.showAlert(new Object({
                    icon: `warning`,
                    title: `<h5>แจ้งเตือน</h4>`,
                    text: `<span class="text-danger">${data.message}</div>`,
                }));
            })
            .catch(async error => {
                await Alerts.showAlert(new Object({
                    icon: `error`,
                    title: `<h5>พบปัญหา</h4>`,
                    text: `<span class="text-danger">${error}</div>`,
                }));
                button.disabled = false;
                button.innerHTML = '<span>ดำเนินการต่อ</span>';
                console.error('Error:', error);
            })
            .finally(async () => {
                button.disabled = false;
                button.innerHTML = '<span>ดำเนินการต่อ</span>';
            });
    });
}