import * as Alerts from "../../helper/alert.js";

export async function initSubmitConfrim() {
    document.getElementById('submitMobileButton').addEventListener('click', async function () {
        const button = this;
        const form = document.getElementById('verifyMobileForm');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังตรวจสอบ...';

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        fetch('/VerifyIdentity/ConfirmMobile', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(async result => {
                if (result.success) {
                    await Alerts.showAlertAndRedirect(new Object({
                        title: 'ส่งข้อมูล',
                        html: `<p>ส่งข้อมูลสําเร็จ</p>`,
                        icon: 'success',
                        url: `/ContractInstallment/ContactInstallment/${result.data.formData}`,
                        confirmButtonText: 'ตกลง'
                    }));
                    return;
                }

                await Alerts.showAlert(new Object({
                    icon: `warning`,
                    title: `<h5>แจ้งเตือน</h4>`,
                    text: `<span class="text-danger">${result.message}</div>`,
                }));
            })
            .catch(async error => {
                await Alerts.showAlert(new Object({
                    icon: `error`,
                    title: `<h5>พบปัญหา</h4>`,
                    text: `<span class="text-danger">${error}</div>`,
                }));
                button.disabled = false;
                button.innerHTML = '<span>ส่งข้อมูล</span>';
                console.error('Error:', error);
            })
            .finally(async () => {
                button.disabled = false;
                button.innerHTML = '<span>ส่งข้อมูล</span>';
            });
    });
}