import * as Alerts from "../../helper/alert.js";
import * as Doms from "./dom.js";

export async function initContact() {
    const checkbox = document.getElementById('consentCheckbox');
    const button = document.getElementById('submitConsentButton');

    checkbox.addEventListener('change', function () {
        // Enable or disable the button based on checkbox state
        button.disabled = !this.checked;
    });
}

// Initialize file input event listeners for verification file uploads
export async function initVerificationFile() {
    document.querySelectorAll('input[type="file"]').forEach(input => {
        input.addEventListener('change', async function () {
            await Doms.handleFileUpload(this);
        });
    });
}

export async function initSubmitConfrim() {
    document.getElementById('submitConsentButton').addEventListener('click', async function () {
        const button = this;
        const form = document.getElementById('confirmConsent');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังตรวจสอบ...';

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        fetch('/ContractInstallment/ConfirmContactInstallment', {
            method: 'POST',
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                // ตรวจสอบว่ามี content หรือไม่
                if (response.status === 204 || response.headers.get('content-length') === '0') {
                    return {}; // return empty object แทน
                }

                return response.json();
            })
            .then(async result => {
                if (result.success) {
                    await Alerts.showAlertAndRedirect(new Object({
                        title: 'ส่งข้อมูล',
                        html: `<p>ส่งข้อมูลสําเร็จ</p>`,
                        icon: 'success',
                        url: `/PaymentInstallment/PaymentInstallment/${result.data.formData}`,
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
                button.innerHTML = '<span>ยืนยันการยินยอม</span>';
                console.error('Error:', error);
            })
            .finally(async () => {
                button.disabled = false;
                button.innerHTML = '<span>ยืนยันการยินยอม</span>';
            });
    });
}