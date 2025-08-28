import * as Alerts from "../../helper/alert.js";
import * as ApiCores from "../../service/apiCores.js";
import * as ApiDatas from "../../service/apiDatas.js";
import * as Doms from "./dom.js";
import * as PageLoaders from '../../helper/pageLoader.js';
import * as Utils from "../../helper/utility.js";

// Event: ฟอร์มค้นหาใบเสนอราคา
export async function inintSearchQuotation() {
    document.getElementById("quotationSearchButton").addEventListener("click", async function () {
        await PageLoaders.pageLoadFadeInModern();
        await Doms.loadQuotationReportList()
            .then(async () => {
                await PageLoaders.pageLoadFadeOutModern(500);
            });
    });
}

// Event: ฟอร์มข้อมูลผู้เอาประกัน
export async function initCustomerForm() {
    const raidiosCustomerType = document.querySelectorAll('input[name="customerTypeRadio"]');
    raidiosCustomerType.forEach(function (radio) {
        radio.addEventListener("change", async function () {
            await Doms.loadPrenameOptions(this.value);
            Doms.handleRadioCustomerChange();
            const element = document.getElementById("installmentPeriodSelect");
            const changeEvent = new Event('change', { bubbles: true });
            element.dispatchEvent(changeEvent);
        });
    });

    const raidiosCustomerTypeCard = document.querySelectorAll('input[name="customerTypeCardRadio"]');
    raidiosCustomerTypeCard.forEach(function (radio) {
        radio.addEventListener("change", async function () {
            Doms.handleRadioCustomerChange();
        });
    });

    const preNameTitleSelect = document.getElementById("preNameTitleSelect"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const firstNameInput = document.getElementById("firstNameInput"); // ชื่อ
    const lastNameInput = document.getElementById("lastNameInput"); // นามสกุล
    const corporationNameInput = document.getElementById("corporationNameInput"); // ชื่อบริษัท
    const prefixSelect = document.getElementById("beneficiaryPrefixSelect"); // คํานําหน้า

    // ดักจับ event change เพื่อจัดการกับการเปลี่ยนแปลงของ select
    preNameTitleSelect.addEventListener("change", function () {
        if (document.getElementById("byInsuredCheckBox").checked) {
            prefixSelect.value = this.value;
        }
    });

    // ดักจับ event keyup เพื่อจัดการกับการเปลี่ยนแปลงของ input
    firstNameInput.addEventListener("keyup", function (e) {
        if (document.getElementById("byInsuredCheckBox").checked) {
            document.getElementById("beneficiaryNameInput").value = `${this.value} ${document.getElementById("lastNameInput").value}`;
        }
    });

    // ดักจับ event keyup เพื่อจัดการกับการเปลี่ยนแปลงของ input
    lastNameInput.addEventListener("keyup", function (e) {
        if (document.getElementById("byInsuredCheckBox").checked) {
            document.getElementById("beneficiaryNameInput").value = `${document.getElementById("firstNameInput").value} ${this.value}`;
        }
    });

    // ดักจับ event keyup เพื่อจัดการกับการเปลี่ยนแปลงของ input
    corporationNameInput.addEventListener("keyup", function (e) {
        if (document.getElementById("byInsuredCheckBox").checked) {
            document.getElementById("beneficiaryNameInput").value = this.value;
        }
    });

    // ดักจับ event keyup และ blur ของ input หมายเลขบัตรประชาชน
    const inputIDCard = document.getElementById("citizenIdInput");

    // ตรวจสอบว่ามี input หมายเลขบัตรประชาชนหรือไม่
    inputIDCard.addEventListener("keypress", function (e) {
        Doms.handleInputCustomerChange(e);
    });

    // ดักจับ event keyup เพื่อจัดการกับการเปลี่ยนแปลงของ input
    inputIDCard.addEventListener("keyup", function (e) {
        Doms.handleInputCustomerChange(e);
    });

    // ดักจับ event blur เพื่อจัดการกับการเปลี่ยนแปลงของ input
    inputIDCard.addEventListener("blur", function (e) {
        Doms.handleInputCustomerChange(e);
    });
}

// Event: ฟอร์มข้อมูลที่อยู่
export async function initCustomerAddress() {
    const custNoInput = document.getElementById("customerAddressNoInput"); // หมายเลขที่อยู่ลูกค้า
    const custProvinceSelect = document.getElementById("customerAddressProvinceSelect"); // จังหวัดที่อยู่ลูกค้า
    const custDistrictSelect = document.getElementById("customerAddressDistrictSelect"); // อำเภอที่อยู่ลูกค้า
    const custSubdistrictSelect = document.getElementById("customerAddressSubDistrictSelect"); // ตำบลที่อยู่ลูกค้า
    const custZipcodeInput = document.getElementById("customerAddressZipCodeInput"); // รหัสไปรษณีย์ที่อยู่ลูกค้า

    const sendNoInput = document.getElementById("sendAddressNoInput"); // หมายเลขที่อยู่จัดส่ง
    const sendProvinceSelect = document.getElementById("sendAddressProvinceSelect"); // จังหวัดที่อยู่จัดส่ง
    const sendDistrictSelect = document.getElementById("sendAddressDistrictSelect"); // อำเภอที่อยู่จัดส่ง
    const sendSubdistrictSelect = document.getElementById("sendAddressSubDistrictSelect"); // ตำบลที่อยู่จัดส่ง
    const sendZipCodeInput = document.getElementById("sendAddressZipCodeInput"); // รหัสไปรษณีย์ที่อยู่จัดส่ง

    const isEditSendCheckBox = document.getElementById("editAddressCheckbox"); // เช็คบ็อกซ์สำหรับแก้ไขที่อยู่จัดส่ง
    const isSendForCustomerRadio = document.getElementById('sendForCustomerRadio'); // เช็คบ็อกซ์สำหรับเลือกที่อยู่จัดส่งโดยลูกค้า

    // ตรวจสอบว่ามีองค์ประกอบที่จำเป็นหรือไม่
    if (!custProvinceSelect || !custDistrictSelect || !custSubdistrictSelect || !custZipcodeInput) {
        console.error("ไม่พบองค์ประกอบที่จำเป็นสำหรับการจัดการที่อยู่ลูกค้า");
        return;
    }

    // ตั้งค่าเริ่มต้นให้กับที่อยู่จัดส่ง
    custNoInput.addEventListener("keyup", (e) => {
        if (!isEditSendCheckBox.checked && isSendForCustomerRadio.checked) {
            sendNoInput.value = e.target.value;
        }
    });

    // ตั้งค่าเริ่มต้นให้กับรหัสไปรษณีย์ที่อยู่จัดส่ง
    custZipcodeInput.addEventListener("keyup", (e) => {
        if (!isEditSendCheckBox.checked && isSendForCustomerRadio.checked) {
            sendZipCodeInput.value = e.target.value;
        }
    });

    // โหลดจังหวัด
    const provinceRes = await ApiCores.fetchProvince();
    custProvinceSelect.innerHTML = `<option value="">กรุณาเลือก</option>` + provinceRes.data.map(v => `<option value="${v.PROVINCE_CODE}">${v.PROV_NAME_T}</option>`).join("");

    // Event: จังหวัด → อำเภอ
    custProvinceSelect.addEventListener("change", async (e) => {
        const provinceCode = e.target.value;
        await Doms.clearSelect(custDistrictSelect, "กรุณาเลือก");
        await Doms.clearSelect(custSubdistrictSelect, "กรุณาเลือก");
        custZipcodeInput.value = ""; // เคลียร์รหัสไปรษณีย์

        // ตั้งค่าอำเภอและตำบลของที่อยู่จัดส่งให้ตรงกับที่อยู่ลูกค้า
        if (provinceCode) {
            const districtRes = await ApiCores.fetchDistrict({ "PROVINCE_CODE": provinceCode });
            custDistrictSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
                districtRes.data.map(v => `<option value="${v.DISTRICT_CODE}">${v.DISTRICT_NAME_T}</option>`).join("");

            // ตั้งค่าอำเภอของที่อยู่จัดส่งให้ตรงกับที่อยู่ลูกค้า
            if (!isEditSendCheckBox.checked && isSendForCustomerRadio.checked) {
                sendProvinceSelect.value = provinceCode; // ตั้งค่า province ของที่อยู่จัดส่งให้ตรงกับที่อยู่ลูกค้า
                // Trigger change บน select2 ด้วย
                const event = new Event("change", { bubbles: true });
                sendProvinceSelect.dispatchEvent(event);
            }
        }
    });

    // Event: อำเภอ → ตำบล
    custDistrictSelect.addEventListener("change", async (e) => {
        const districtCode = e.target.value; // รหัสอำเภอที่ถูกเลือก
        await Doms.clearSelect(custSubdistrictSelect, "กรุณาเลือก"); // เคลียร์ตำบล
        custZipcodeInput.value = ""; // เคลียร์รหัสไปรษณีย์

        // ตั้งค่า subdistrict ของที่อยู่จัดส่งให้ตรงกับที่อยู่ลูกค้า
        if (districtCode) {
            const subdistrictRes = await ApiCores.fetchSubDistrict({ "PROVINCE_CODE": custProvinceSelect.value, "DISTRICT_CODE": districtCode }); // ดึงข้อมูลตำบลตามรหัสอำเภอ
            custSubdistrictSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
                subdistrictRes.data.map(v => `<option value="${v.I_SUBDISTRICT}" zipcode="${v.I_ZIPCODE}" >${v.SUBDISTRICT}</option>`).join("");

            if (!isEditSendCheckBox.checked && isSendForCustomerRadio.checked) {
                sendDistrictSelect.value = districtCode; // ตั้งค่า district ของที่อยู่จัดส่งให้ตรงกับที่อยู่ลูกค้า
                // Trigger change บน select2 ด้วย
                const event = new Event("change", { bubbles: true });
                sendDistrictSelect.dispatchEvent(event);
            }
        }
    });

    // Event: ตำบล → รหัสไปรษณีย์
    custSubdistrictSelect.addEventListener("change", (e) => {
        const subdistrictCode = e.target.value; // รหัสตำบลที่ถูกเลือก
        custZipcodeInput.value = e.target.selectedOptions[0].getAttribute("zipcode") || ""; // ดึงรหัสไปรษณีย์จาก attribute

        if (!isEditSendCheckBox.checked && isSendForCustomerRadio.checked) {
            sendSubdistrictSelect.value = subdistrictCode; // ตั้งค่า subdistrict ของที่อยู่จัดส่งให้ตรงกับที่อยู่ลูกค้า
            // Trigger change บน select2 ด้วย
            const event = new Event("change", { bubbles: true });
            sendSubdistrictSelect.dispatchEvent(event);
        }
    });
}

// Event: ที่อยู่จัดส่งเอกสาร
export async function initSendDocAddress() {
    const sendProvinceSelect = document.getElementById("sendAddressProvinceSelect"); // จังหวัดที่อยู่จัดส่งเอกสาร
    const sendDistrictSelect = document.getElementById("sendAddressDistrictSelect"); // อำเภอที่อยู่จัดส่งเอกสาร
    const SendSubdistrictSelect = document.getElementById("sendAddressSubDistrictSelect"); // ตำบลที่อยู่จัดส่งเอกสาร
    const sendZipCodeInput = document.getElementById("sendAddressZipCodeInput"); // รหัสไปรษณีย์ที่อยู่จัดส่งเอกสาร
    const radios = document.querySelectorAll('input[name="sendByAddressRadio"]'); // เช็คบ็อกซ์สำหรับเลือกที่อยู่จัดส่งเอกสาร
    const editAddressCheckbox = document.getElementById("editAddressCheckbox"); // เช็คบ็อกซ์สำหรับแก้ไขที่อยู่จัดส่ง

    // ตรวจสอบว่ามีองค์ประกอบที่จำเป็นหรือไม่
    radios.forEach(function (radio) {
        radio.addEventListener("change", async function () {
            this.value === "A" ? await Doms.loadSendDocAddressByAgent() : await Doms.loadSendDocAddressByCustomer(); // โหลดที่อยู่จัดส่งเอกสารตามตัวเลือก
        });
    });

    // Event: จังหวัด → อำเภอ
    sendProvinceSelect.addEventListener("change", async (e) => {
        const provinceCode = e.target.value; // รหัสจังหวัดที่ถูกเลือก
        await Doms.clearSelect(sendDistrictSelect, "กรุณาเลือก"); // เคลียร์อำเภอ
        await Doms.clearSelect(SendSubdistrictSelect, "กรุณาเลือก"); // เคลียร์ตำบล
        sendZipCodeInput.value = ""; // เคลียร์รหัสไปรษณีย์

        // ตั้งค่าอำเภอและตำบลของที่อยู่จัดส่งเอกสาร
        if (provinceCode) {
            const districtRes = await ApiCores.fetchDistrict({ "PROVINCE_CODE": provinceCode }); // ดึงข้อมูลอำเภอตามรหัสจังหวัด
            sendDistrictSelect.innerHTML = `<option value="">กรุณาเลือก</option>` + districtRes.data.map(v => `<option value="${v.DISTRICT_CODE}">${v.DISTRICT_NAME_T}</option>`).join(""); // สร้างตัวเลือกอำเภอ
        }
    });

    // Event: อำเภอ → ตำบล
    sendDistrictSelect.addEventListener("change", async (e) => {
        const districtCode = e.target.value; // รหัสอำเภอที่ถูกเลือก
        await Doms.clearSelect(SendSubdistrictSelect, "กรุณาเลือก"); // เคลียร์ตำบล
        sendZipCodeInput.value = ""; // เคลียร์รหัสไปรษณีย์

        // ตั้งค่า subdistrict ของที่อยู่จัดส่งเอกสาร
        if (districtCode) {
            const subdistrictRes = await ApiCores.fetchSubDistrict({ "PROVINCE_CODE": sendProvinceSelect.value, "DISTRICT_CODE": districtCode }); // ดึงข้อมูลตำบลตามรหัสอำเภอ
            SendSubdistrictSelect.innerHTML = `<option value="">กรุณาเลือก</option>` + subdistrictRes.data.map(v => `<option value="${v.I_SUBDISTRICT}" zipcode="${v.I_ZIPCODE}" >${v.SUBDISTRICT}</option>`).join(""); // สร้างตัวเลือกตำบล
        }
    });

    // Event: ตำบล → รหัสไปรษณีย์
    SendSubdistrictSelect.addEventListener("change", (e) => {
        sendZipCodeInput.value = e.target.selectedOptions[0].getAttribute("zipcode") || ""; // ดึงรหัสไปรษณีย์จาก attribute
    });

    const isSendForAgentRadio = document.getElementById('sendForAgentRadio'); // เช็คบ็อกซ์สำหรับเลือกที่อยู่จัดส่งโดยตัวแทน

    // ตั้งค่าเริ่มต้นให้กับที่อยู่จัดส่งเอกสาร
    editAddressCheckbox.addEventListener("change", async function () {
        const addressFields = [
            { id: "sendAddressNoInput", isInput: true },
            { id: "sendAddressProvinceSelect" },
            { id: "sendAddressDistrictSelect" },
            { id: "sendAddressSubDistrictSelect" },
            { id: "sendAddressZipCodeInput", isInput: true }
        ];

        addressFields.forEach(field => {
            const element = document.getElementById(field.id);
            if (this.checked) {
                element.classList.remove("readonly");
                if (field.isInput) element.readOnly = false;
            } else {
                element.classList.add("readonly");
                if (field.isInput) element.readOnly = true;
            }
        });

        // ถ้าเลือกแก้ไขที่อยู่จัดส่งเอกสาร ให้โหลดที่อยู่จัดส่งเอกสารตามตัวแทนหรือลูกค้า
        isSendForAgentRadio.checked ? await Doms.loadSendDocAddressByAgent() : await Doms.loadSendDocAddressByCustomer();
    });
}

// Event: ฟอร์มข้อมูลความรับผลประโยชน์
export async function initCoverageInformationForm() {
    const byInsuredCheckBox = document.getElementById("byInsuredCheckBox"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    byInsuredCheckBox.addEventListener("click", function () {
        const prefixSelect = document.getElementById("beneficiaryPrefixSelect");
        const preNameTitleSelect = document.getElementById("preNameTitleSelect");
        const nameInput = document.getElementById("beneficiaryNameInput");
        const firstNameInput = document.getElementById("firstNameInput");
        const lastNameInput = document.getElementById("lastNameInput");
        const corporationNameInput = document.getElementById("corporationNameInput");
        const selectedValue = document.querySelector('[name="customerTypeRadio"]:checked').value;

        if (byInsuredCheckBox.checked) {
            // ถ้าเลือกให้ผู้เอาประกันภัยเป็นผู้รับผลประโยชน์ → ปิดการแก้ไข
            prefixSelect.value = preNameTitleSelect.value; // ถ้าเลือกผู้รับผลประโยชน์ ให้ใช้ชื่อผู้รับผลประโยชน์
            nameInput.value = selectedValue === "N" ? `${firstNameInput.value} ${lastNameInput.value}` : `${corporationNameInput.value}`; // ถ้าเลือกผู้รับผลประโยชน์ ให้ใช้ชื่อผู้รับผลประโยชน์
            prefixSelect.classList.add("readonly"); // ปิดการแก้ไข
            nameInput.classList.add("readonly"); // ปิดการแก้ไข
        } else {
            // ถ้าไม่เลือก → เปิดให้แก้ไขได้
            prefixSelect.classList.remove("readonly"); // เปิดการแก้ไข
            nameInput.classList.remove("readonly"); // เปิดการแก้ไข
        }
    });
}

// Event: ฟอร์มข้อมูลการตรวจรถ
export async function initVehicleInspectionForm() {
    // เมื่อเลือก "ตรวจโดยตัวแทน"
    document.getElementById('agentInspectionRadio').addEventListener('click', () => {
        Doms.toggleInspectionMode({
            collapseAction: 'toggle',
            showMobile: false,
            showAgent: true,
            showWounds: true,
            disableRadioId: 'insuranceInspectionRadio'
        });
    });

    // เมื่อเลือก "ตรวจโดยบริษัทประกัน"
    document.getElementById('insuranceInspectionRadio').addEventListener('click', () => {
        Doms.toggleInspectionMode({
            collapseAction: 'hide',
            showMobile: true,
            showAgent: false,
            showWounds: false,
            disableRadioId: 'agentInspectionRadio'
        });
    });

    // เมื่อเลือกตัวแทนตรวจรถ
    document.getElementById("inspectionAgentSelect").addEventListener("change", function () {
        const woundsInput = document.getElementById("inspectionCarWoundsInput");
        const selectedValue = this.value;

        if (selectedValue === "Y") {
            woundsInput.classList.remove("readonly");
        } else {
            woundsInput.value = "";
            woundsInput.classList.add("readonly");
        }

        Doms.updateCarInspectionSummary();
    });

    // อัปเดตสรุปผลการตรวจรถเมื่อมีการพิมพ์ข้อมูล
    ["inspectionMobileInput", "inspectionCarWoundsInput", "inspectionRemarkInput"].forEach(id => {
        document.getElementById(id).addEventListener("keyup", Doms.updateCarInspectionSummary);
    });

    document.querySelectorAll('input[type="file"]').forEach(input => {
        input.addEventListener('change', async function () {
            await Doms.handleFileUpload(this);
        });
    });
}

// Event: ฟอร์มข้อมูลการส่งกรมธรรม์
export async function initDocumentReceivingChannels() {
    // สร้าง collapse object สำหรับ ePolicy และ paper แบบไม่ toggle อัตโนมัติ
    const ePolicyCollapse = new bootstrap.Collapse(document.getElementById('ePolicyCollapse'), { toggle: false });
    const paperCollapse = new bootstrap.Collapse(document.getElementById('paperCollapse'), { toggle: false });

    // เพิ่ม event listener สำหรับเลือกส่งกรมธรรม์แบบ ePolicy
    document.getElementById('sendEPolicyRadio').addEventListener('click', () => {
        paperCollapse.hide();     // ซ่อน paper
        ePolicyCollapse.toggle(); // แสดง/ซ่อน ePolicy
    });

    // เพิ่ม event listener สำหรับเลือกส่งกรมธรรม์แบบเอกสารกระดาษ
    document.getElementById('sendPaperRadio').addEventListener('click', () => {
        ePolicyCollapse.hide();   // ซ่อน ePolicy
        paperCollapse.toggle();   // แสดง/ซ่อน paper
    });
}

// Event: ฟอร์มข้อมูลการชำระเงิน
export async function initPaymentChannels() {
    // เพิ่ม event listener ให้ radio buttons สำหรับเลือกช่องทางการชำระเงินออนไลน์
    document.getElementsByName("channelPaymentOnlineRadio").forEach(radio => {
        radio.addEventListener("change", Doms.handleOnlinePaymentChannelChange); // เมื่อเลือกช่องทางการชำระเงินออนไลน์
    });

    // เพิ่ม event listener ให้ radio buttons สำหรับเลือกช่องทางการชำระเงินออนไลน์
    document.getElementsByName("channelPaymentRadio").forEach(radio => {
        radio.addEventListener("change", Doms.handlePaymentOnlineChannelChange);
    });

    document.getElementById("installmentPeriodSelect").addEventListener("change", async function () {
        const NetPremiumInput = document.getElementById("NetPremiumInput"); // ยอดสุทธิ
        const TotalPremiumInput = document.getElementById("TotalPremiumInput"); // ยอดรวม
        const TotalCompulsoryInput = document.getElementById("TotalCompulsoryInput"); // ยอดพรบ.
        const firstInstallmentInput = document.getElementById("firstInstallmentInput"); // งวดแรก
        const nextInstallmentInput = document.getElementById("nextInstallmentInput"); // งวดถัดไป
        const customerTypeRadio = document.querySelector('[name="customerTypeRadio"]:checked'); // ประเภทลูกค้า

        if (NetPremiumInput.value === "" || TotalPremiumInput.value === "" || this.value === "") return;

        let data = new Object({
            "VOL_NET_PREMIUM_AMT": Utils.convertToNumber(NetPremiumInput.value) ?? 0.00,
            "VOL_TOTAL_PREMIUM_AMT": Utils.convertToNumber(TotalPremiumInput.value) ?? 0.00,
            "COP_TOTAL_PREMIUM_AMT": Utils.convertToNumber(TotalCompulsoryInput.value) ?? 0.00,
            "NUM_INSTALL": this.value,
            "FLG_PERSON": customerTypeRadio.value
        });

        const response = await ApiCores.FetchCalculatePeriod(data); // ดึงข้อมูลการชำระเงิน
        // แสดงข้อมูลการชำระเงิน
        if (response.data.length > 0) {
            firstInstallmentInput.value = Utils.formatToDisplayCurrency(response.data[0].PREMIUM_AMT + response.data[0].WTAX_AMT); // ค่าเริ่มต้น
            nextInstallmentInput.value = Utils.formatToDisplayCurrency(response.data[1].PREMIUM_AMT + response.data[1].WTAX_AMT); // ค่าต่อ
        }
    });
}

export async function initOrderMotorDataTables(tableEl, tableInstance) {
    const tbody = tableEl.querySelector("tbody"); // ดึง tbody ของตาราง
    let collapsedGroups = {}; // วัตถุสำหรับเก็บสถานะการยุบกลุ่มแถว

    // ตั้งค่า collapsedGroups จาก data attribute ของ tbody
    tbody.addEventListener("click", async (event) => {
        const button = event.target.closest("button"); // ดึงปุ่มที่ถูกคลิก
        if (!button || button.closest("tr.dtrg-start")) return; // ถ้าไม่ใช่ปุ่มแจ้งงานหรือไม่อยู่ในแถวกลุ่ม

        const row = tableInstance.row(button.closest("tr")); // ดึงแถวที่ถูกคลิก
        const dataTable = row.data(); // ดึงข้อมูลจากแถวที่ถูกคลิก

        try {
            await PageLoaders.pageLoadFadeInModern();

            // ตรวจสอบว่าใบเสนอราคาได้ถูกใช้ในใบคำขอหรือไม่
            const inquiryQuotationResult = await ApiCores.fetchQuotationDetail({ QUOTATION_NO: dataTable.quo_number }); // ดึงข้อมูลใบเสนอราคา

            // ถ้าใบเสนอราคาได้ถูกใช้ในใบคำขอแล้ว ให้แสดงข้อความเตือน
            if (inquiryQuotationResult.data !== null) {
                await PageLoaders.pageLoadFadeOutModern(500);
                await Alerts.showAlert(new Object({
                    icon: `warning`,
                    title: `<h5>ไม่สามารถใช้ใบเสนอราคา</h5> <br> <h4><b>${dataTable.quo_number}</b></h4>`,
                    text: `<span class="text-danger">เนื่องจากใบเสนอราคานี้ได้ออกเลขใบคำขอเรียบร้อยแล้ว</div>`,
                }));
                return;
            }

            // ดึงข้อมูลแผนหลักและรายละเอียดใบเสนอราคา
            const inquiryMasterPlanResult = await ApiDatas.fetchMasterPlanDetails({
                company_code: dataTable.company_code, // รหัสบริษัท
                tm_product_code: dataTable.tm_product_code, // รหัสผลิตภัณฑ์
                coverage_code: dataTable.coverage_code // รหัสความคุ้มครอง
            });

            if (inquiryMasterPlanResult.data == null) {
                await PageLoaders.pageLoadFadeOutModern(500);
                await Alerts.showAlert(new Object({
                    icon: `warning`,
                    title: `<h5>ไม่พบข้อมูลแผนหลักและรายละเอียดใบเสนอราคา</h5> <br> <h4><b>${dataTable.quo_number}</b></h4>`,
                    text: `<span class="text-danger">กรุณาตรวจสอบข้อมูล</div>`,
                }));
                return;
            }

            // ถ้าไม่พบแผนหลัก ให้แสดงข้อความเตือน
            const inquiryQuotatuinDetailResult = await ApiDatas.fetchQuotationReportDetail({
                id: dataTable.id, // รหัสใบเสนอราคา
                quo_number: dataTable.quo_number, // เลขที่ใบเสนอราคา
                premiums_id: dataTable.premiums_id, // รหัสเบี้ยประกันภัย
                company_code: dataTable.company_code, // รหัสบริษัท
                tm_product_code: dataTable.tm_product_code // รหัสผลิตภัณฑ์
            });

            // ถ้าไม่พบรายละเอียดใบเสนอราคา ให้แสดงข้อความเตือน
            if (inquiryQuotatuinDetailResult.data.length === 0) {
                await PageLoaders.pageLoadFadeOutModern(500);
                // แสดงข้อความเตือนถ้าไม่พบข้อมูลใบเสนอราคา
                await Alerts.showAlert(new Object({
                    icon: `warning`,
                    title: `<h5>ไม่พบรายละเอียดใบเสนอราคา</h5> <br> <h4><b>${dataTable.quo_number}</b></h4>`,
                    text: `<span class="text-danger">กรุณาตรวจสอบข้อมูลอีกครั้ง</div>`,
                }));
                return;
            }

            // ถ้าไม่พบแผนหลัก ให้แสดงข้อความเตือน
            const event = new Event("click", { bubbles: true });
            document.getElementById("closeOrderModalButton").dispatchEvent(event); // ปิดโมดอลที่เปิดอยู่

            await Doms.loadData(inquiryQuotatuinDetailResult.data[0], dataTable)
                .then(async () => {
                    await PageLoaders.pageLoadFadeOutModern(500);
                }); // โหลดข้อมูลใบเสนอราคาไปยังหน้า
        } catch (err) {
            console.error(err);
            await PageLoaders.pageLoadFadeOutModern(500);
        }
    });

    // ✅ collapse group row
    tbody.addEventListener("click", (event) => {
        const target = event.target.closest("tr.dtrg-start");
        if (!target) return;

        const group = target.dataset.name;
        collapsedGroups[group] = !collapsedGroups[group];
        tableInstance.draw(false);
    });
}

export async function initSubmitMotor() {
    // บันทึกข้อมูลทั้งหมด
    document.getElementById('submitMotorButton').addEventListener('click', async function () {
        await PageLoaders.pageLoadFadeInModern();

        const button = this;
        const form = document.getElementById('newMotorSubmitForm');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังบันทึก...';

        const vehicleCodeSelect = document.getElementById("vehicleCodeSelect");
        const vehicleCodeSelectedOption = vehicleCodeSelect.options[vehicleCodeSelect.selectedIndex];
        const carSeat = vehicleCodeSelectedOption.getAttribute("seat");
        const subInsureType = vehicleCodeSelectedOption.getAttribute("cmi");
        const preNameTitleSelect2 = document.getElementById("preNameTitleSelect");
        const preNameSelectOption = preNameTitleSelect2.options[preNameTitleSelect2.selectedIndex];
        const customerGender = preNameSelectOption.getAttribute("gender");
        const beneficiaryPrefixSelect = document.getElementById("beneficiaryPrefixSelect");
        const beneficiarySelectOption = beneficiaryPrefixSelect.options[beneficiaryPrefixSelect.selectedIndex].text;
        const preNameTitleSelect = document.getElementById("preNameTitleSelect");
        const preNameTitleSelectedText = preNameTitleSelect.options[preNameTitleSelect.selectedIndex].text;
        const customerAddressNoInput = document.getElementById("customerAddressNoInput").value;
        const customerAddressProvinceSelect = document.getElementById("customerAddressProvinceSelect");
        const customerAddressProvinceSelectedText = customerAddressProvinceSelect.options[customerAddressProvinceSelect.selectedIndex].text;
        const customerAddressDistrictSelect = document.getElementById("customerAddressDistrictSelect");
        const customerAddressDistrictSelectedText = customerAddressDistrictSelect.options[customerAddressDistrictSelect.selectedIndex].text;
        const customerAddressSubDistrictSelect = document.getElementById("customerAddressSubDistrictSelect");
        const customerAddressSubDistrictSelectedText = customerAddressSubDistrictSelect.options[customerAddressSubDistrictSelect.selectedIndex].text;
        const customerAddressZipCodeInput = document.getElementById("customerAddressZipCodeInput").value;
        const registrationCarProvinceSelect = document.getElementById("registrationCarProvinceSelect");
        const registrationCarProvinceSelectText = registrationCarProvinceSelect.options[registrationCarProvinceSelect.selectedIndex].text;

        const carBrandSelect = document.getElementById("carBrandSelect");
        const carBrandSelectText = carBrandSelect.options[carBrandSelect.selectedIndex].text;
        const carModelSelect = document.getElementById("carModelSelect");
        const carModelSelectText = carModelSelect.options[carModelSelect.selectedIndex].text;

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        // เพิ่มข้อมูลเพิ่มเติม (ถ้าต้องการ)
        formData.append('carSeat', carSeat);
        formData.append('customerGender', customerGender);
        formData.append('prefixBeneficiary', beneficiarySelectOption);
        formData.append('subInsureTypeCompulsory', subInsureType);
        formData.append('preNameTitle', preNameTitleSelectedText);
        formData.append('CarProvince', registrationCarProvinceSelectText);
        formData.append('Address', `${customerAddressNoInput} ${customerAddressSubDistrictSelectedText} ${customerAddressDistrictSelectedText} ${customerAddressProvinceSelectedText} ${customerAddressZipCodeInput}`);
        formData.append('CarBrand', carBrandSelectText);
        formData.append('CarModel', carModelSelectText);

        fetch('/SubmitMotorNew/SubmitMotor', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(async data => {
                await PageLoaders.pageLoadFadeOutModern(500);
                if (data.success) {
                    let result = await Alerts.showConfirmationDialog(new Object({
                        title: 'แจ้งเตือน',
                        text: `<span>${data.message}</span>`,
                        icon: 'warning',
                        confirmButtonText: 'ตกลง',
                        denyButtonText: 'ยกเลิก',
                    }));

                    if (result.isConfirmed && data.channel && data.channel.trim() !== '') {
                        const channel = data.channel.trim();
                        return window.location.href = channel;
                    }
                }

                await Alerts.showAlert(new Object({
                    icon: `warning`,
                    title: `แจ้งเตือน`,
                    text: `<span class="text-danger">${data.message}</div>`,
                }));
            })
            .catch(async error => {
                button.disabled = false;
                button.innerHTML = '<i class="fa-solid fa-check fa-xl"></i> <span>แจ้งงาน</span>';
                console.error('Error:', error);
            })
            .finally(async () => {
                await PageLoaders.pageLoadFadeOutModern(500);
                button.disabled = false;
                button.innerHTML = '<i class="fa-solid fa-check fa-xl"></i> <span>แจ้งงาน</span>';
            });
    });
}