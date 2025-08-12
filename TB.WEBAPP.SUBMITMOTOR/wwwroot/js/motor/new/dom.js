//จัดการ DOM Event, User Interaction
import * as Alert from "../../helper/alert.js";
import * as DateUtils from "../../helper/dateUtils.js";
import * as Pageloads from '../../helper/pageLoader.js';
import * as Utility from "../../helper/utility.js";
import * as Validations from "../../helper/validations.js";
import * as ApiCores from "../../service/apiCores.js";
import * as ApiDatas from "../../service/apiDatas.js";
import * as Events from "./events.js";

const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]')
const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl))

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

export async function initForm() {
    // โหลดหน้าเว็บ
    await Pageloads.pageLoadFadeInModern(); // โหลดหน้าเว็บ

    // โหลด DOM
    await Pageloads.delay(2000).then(async () => {
        // ✅ โหลด DOM ก่อน
        await loadQuotationReportList(); // โหลดรายการใบเสนอราคา
        await loadPrenameOptions(); // โหลดข้อมูลคำนำหน้า
        await loadOccupationOptions(); // โหลดข้อมูลอาชีพ
        await loadSendDocAddressByAgent(); // โหลดที่อยู่สำหรับส่งเอกสารตามตัวแทน
        await loadCarRegisterProvinceOptions(); // โหลดจังหวัดจดทะเบียนรถ
        await loadCarBrandOptions(); // โหลดยี่ห้อรถ
        await loadCarColorOptions(); // โหลดสีรถ
        await loadDatePicker(); // โหลด datepicker

        // ✅ ค่อย bind event หลัง DOM มี แล้ว
        await Events.inintSearchQuotation(); // Event: ฟอร์มค้นหาใบเสนอราคา
        await Events.initCustomerForm(); // Event: ฟอร์มข้อมูลลูกค้า
        await Events.initCustomerAddress(); // Event: ฟอร์มข้อมูลที่อยู่ลูกค้า
        await Events.initSendDocAddress(); // Event: ฟอร์มข้อมูลที่อยู่ส่งเอกสาร
        await Events.initCoverageInformationForm(); // Event: ฟอร์มข้อมูลความเสี่ยง
        await Events.initVehicleInspectionForm(); // Event: ฟอร์มข้อมูลการตรวจรถ
        await Events.initDocumentReceivingChannels(); // Event: ฟอร์มข้อมูลการส่งเอกสาร
        await Events.initPaymentChannels(); // Event: ฟอร์มข้อมูลการชำระเงิน
        await Events.initSubmitMotor(); // Event: ฟอร์มใบเสนอราคา
    }).finally(async () => {
        // โหลด fade out
        await Pageloads.pageLoadFadeOutModern(500); // โหลด fade out
    });
}

// โหลด datepicker
export async function loadDatePicker() {
    // สร้าง datepicker สำหรับวันเกิด พร้อมคำนวณอายุแสดงใน span#ageSpan
    DateUtils.createThaiDatePicker("#birthDateCustomerInput", {
        minDate: dayjs().add(-100, "year").toDate(), // ตั้งค่าวันเริ่มต้นเป็น 100 ปีก่อนวันปัจจุบัน
        maxDate: dayjs().add(-1, "year").toDate(), // ตั้งค่าวันสิ้นสุดเป็น 1 ปีก่อนวันปัจจุบัน
        onChange: ([selectedDate]) => {
            const age = Utility.calculateAge(selectedDate); // คํานวณอายุ
            document.getElementById("ageSpan").innerText = age; // แสดงอายุใน span
        }
    });

    // สร้าง datepicker สำหรับวันที่จดทะเบียน พร้อมคำนวณอายุแสดงใน span#ageSpan
    DateUtils.createThaiDatePicker("#registerDateCustomerInput", {
        minDate: dayjs().add(-100, "year").toDate(), // ตั้งค่าวันเริ่มต้นเป็น 100 ปีก่อนวันปัจจุบัน
        maxDate: dayjs().add(-1, "year").toDate(), // ตั้งค่าวันสิ้นสุดเป็น 1 ปีก่อนวันปัจจุบัน
    });

    // สร้าง DatePicker สำหรับวันเริ่มต้น
    DateUtils.createThaiDatePicker("#voluntaryStartDateInput", {
        minDate: dayjs().toDate(), // ตั้งค่าวันเริ่มต้นเป็นวันปัจจุบัน
        maxDate: dayjs().add(90, "day").toDate(), // ตั้งค่าวันสิ้นสุดเป็น 90 วันหลังวันปัจจุบัน
        defaultDate: new Date(), // ตั้งค่าวันเริ่มต้นเป็นวันปัจจุบัน
        onChange: ([selectedDate]) => {
            if (!selectedDate) return; // ถ้าวันเริ่มต้นไม่ถูกเลือก

            const newExpireDate = Utility.addOneYear(selectedDate); // อัปเดตวันที่หมดอายุเป็น 1 ปีหลังวันที่เริ่มต้น
            const expireInput = document.getElementById("voluntaryExpireDateInput"); // อัปเดตวันที่หมดอายุ

            if (!expireInput?._flatpickr) return; // ถ้า flatpickr ยังไม่ถูกสร้าง

            // อัปเดตวันที่หมดอายุเป็น 1 ปีหลังวันที่เริ่มต้น
            expireInput._flatpickr.setDate(newExpireDate, false, "d/m/Y"); // อัปเดตวันที่หมดอายุ
        },
    });

    // สร้าง datepicker สำหรับวันหมดอายุ
    DateUtils.createThaiDatePicker("#voluntaryExpireDateInput", {
        defaultDate: dayjs().add(1, "year").toDate(), // ตั้งค่าวันเริ่มต้นเป็น 1 ปีหลังวันปัจจุบัน
        locale: "th", // ตั้งค่าเป็นภาษาไทย
        dateFormat: "d/m/Y", // ตั้งค่ารูปแบบวันที่
        clickOpens: false, // กำหนดไม่ให้แก้ไขเอง (ถ้าต้องการ)
    });

    // สร้าง datepicker สำหรับวันเริ่มต้น
    DateUtils.createThaiDatePicker("#compulsoryStartDateInput", {
        minDate: dayjs().toDate(), // ตั้งค่าวันเริ่มต้นเป็นวันปัจจุบัน
        maxDate: dayjs().add(90, "day").toDate(), // ตั้งค่าวันสิ้นสุดเป็น 90 วันหลังวันปัจจุบัน
        defaultDate: new Date(), // ตั้งค่าวันเริ่มต้นเป็นวันปัจจุบัน
        onChange: ([selectedDate]) => {
            if (!selectedDate) return; // ถ้าวันที่เริ่มต้นไม่ถูกเลือก

            const newExpireDate = Utility.addOneYear(selectedDate); // อัปเดตวันที่หมดอายุเป็น 1 ปีหลังวันที่เริ่มต้น
            const expireInput = document.getElementById("compulsoryExpireDateInput"); // อัปเดตวันที่หมดอายุ

            if (!expireInput?._flatpickr) return; // ถ้า flatpickr ยังไม่ถูกสร้าง

            // อัปเดตวันที่หมดอายุเป็น 1 ปีหลังวันที่เริ่มต้น
            expireInput._flatpickr.setDate(newExpireDate, false, "d/m/Y"); // อัปเดตวันที่หมดอายุ
        },
    });

    // สร้าง datepicker สำหรับวันหมดอายุ
    DateUtils.createThaiDatePicker("#compulsoryExpireDateInput", {
        defaultDate: dayjs().add(1, "year").toDate(), // ตั้งค่าวันเริ่มต้นเป็น 1 ปีหลังวันปัจจุบัน
        locale: "th", // ตั้งค่าเป็นภาษาไทย
        dateFormat: "d/m/Y", // ตั้งค่ารูปแบบวันที่
        clickOpens: false, // กำหนดไม่ให้แก้ไขเอง (ถ้าต้องการ)
    });

    // สร้าง datepicker สำหรับวันเริ่มต้น
    DateUtils.createThaiDatePicker("#quotationStartDateSearchInput", {
        defaultDate: new Date(), // ตั้งค่าวันเริ่มต้นเป็นวันปัจจุบัน
        locale: "th", // ตั้งค่าเป็นภาษาไทย
        dateFormat: "d/m/Y", // ตั้งค่ารูปแบบวันที่
        clickOpens: false, // กำหนดไม่ให้แก้ไขเอง (ถ้าต้องการ)
        onChange: ([selectedDate]) => {
            if (!selectedDate) return; // ถ้าวันที่เริ่มต้นไม่ถูกเลือก

            const dateStart = selectedDate;
            const dateEnd = DateUtils.convertStringToDate(document.getElementById("quotationEndDateSearchInput").value);
            const isValid = Validations.isValidateDateBetween(dateStart, dateEnd);

            if (!isValid.valid) {
                Alert.showAlert(new Object({
                    icon: `warning`,
                    title: `<h4><b>แจ้งเตือน</b></h4>`,
                    text: `<span class="text-danger">${isValid.message}</div>`,
                }));

                const input = document.getElementById("quotationStartDateSearchInput");
                input.value = DateUtils.convertDateToString(dateEnd);

                // ถ้าใช้ Flatpickr
                if (input._flatpickr) {
                    input._flatpickr.setDate(dateEnd);
                }
            }
        },
    });

    // สร้าง datepicker สำหรับวันสิ้นสุด
    DateUtils.createThaiDatePicker("#quotationEndDateSearchInput", {
        defaultDate: new Date(), // ตั้งค่าวันเริ่มต้นเป็นวันปัจจุบัน
        locale: "th", // ตั้งค่าเป็นภาษาไทย
        dateFormat: "d/m/Y", // ตั้งค่ารูปแบบวันที่
        clickOpens: false, // กำหนดไม่ให้แก้ไขเอง (ถ้าต้องการ)
        onChange: ([selectedDate]) => {
            if (!selectedDate) return; // ถ้าวันที่เริ่มต้นไม่ถูกเลือก

            const dateStart = DateUtils.convertStringToDate(document.getElementById("quotationStartDateSearchInput").value);
            const dateEnd = selectedDate;
            const isValid = Validations.isValidateDateBetween(dateStart, dateEnd);

            if (!isValid.valid) {
                Alert.showAlert(new Object({
                    icon: `warning`,
                    title: `<h4><b>แจ้งเตือน</b></h4>`,
                    text: `<span class="text-danger">${isValid.message}</div>`,
                }));

                const input = document.getElementById("quotationEndDateSearchInput");
                input.value = DateUtils.convertDateToString(dateStart);

                // ถ้าใช้ Flatpickr
                if (input._flatpickr) {
                    input._flatpickr.setDate(dateStart);
                }
            }
        },
    });
}

// โหลดรายการใบเสนอราคา
export async function loadQuotationReportList() {
    try {
        //// โหลดหน้าเว็บ
        //await Pageloads.pageLoadFadeInModern(); // โหลดหน้าเว็บ

        //// โหลด DOM
        //await Pageloads.delay(2000).then(async () => {
            const registrationCarSearchInput = document.getElementById("registrationCarSearchInput"); // เลขทะเบียน
            const customerSearchInput = document.getElementById("customerSearchInput"); // ชื่อลูกค้า
            const quotationStartDateSearchInput = document.getElementById("quotationStartDateSearchInput"); // วันเริ่มต้น
            const quotationEndDateSearchInput = document.getElementById("quotationEndDateSearchInput"); // วันสิ้นสุด

            // ดึงค่าวันที่จาก flatpickr instance
            let startDate = null;
            let endDate = null;

            // ตรวจสอบว่า flatpickr มีค่าหรือไม่
            if (quotationStartDateSearchInput._flatpickr) {
                const selectedStartDate = quotationStartDateSearchInput._flatpickr.selectedDates[0]; // วันเริ่มต้น
                if (selectedStartDate) {
                    startDate = dayjs(selectedStartDate).format('YYYY-MM-DD'); // วันเริ่มต้น
                }
            }

            // ตรวจสอบว่า flatpickr มีค่าหรือไม่
            if (quotationEndDateSearchInput._flatpickr) {
                const selectedEndDate = quotationEndDateSearchInput._flatpickr.selectedDates[0]; // วันสิ้นสุด
                if (selectedEndDate) {
                    endDate = dayjs(selectedEndDate).format('YYYY-MM-DD'); // วันสิ้นสุด
                }
            }

            // ถ้าไม่มีค่าให้ใช้ค่า default
            if (!startDate) {
                startDate = dayjs().format('YYYY-MM-DD'); // วันปัจจุบัน
            }
            if (!endDate) {
                endDate = dayjs().format('YYYY-MM-DD'); // วันปัจจุบัน
            }

        const result = await ApiDatas.fetchQuotationReportList({
                name: registrationCarSearchInput.value, // ชื่อ-สกุล
                vehicle_license: customerSearchInput.value, // เลขทะเบียนรถ
                company_code: "TNI", // บริษัท
                coverage_code: "", // ประเภทประกัน
                date_start: "2025-01-30", // วันเริ่มต้น
                date_end: "2025-06-30" // วันสิ้นสุด
            });

            const tableEl = document.querySelector("#orderMotorTable"); // เลือกตาราง
            let tableInstance = await initOrderMotorTable(tableEl, result.data); // สร้างตาราง

            await Events.initOrderMotorDataTables(tableEl, tableInstance); // ตั้งค่าเหตุการณ์ให้กับตาราง
        //}).finally(async () => {
        //    await Pageloads.pageLoadFadeOutModern(500); // โหลดหน้าเว็บ
        /*});        */
    } catch (err) {
        console.error("โหลดข้อมูล", err);
    }
}

// สร้างตารางใบเสนอราคา
export async function initOrderMotorTable(tableEl, result) {
    let collapsedGroups = {};
    let tableInstance;
    try {
        if (DataTable.isDataTable(tableEl)) {
            tableInstance = $("#orderMotorTable").DataTable();
            tableInstance.clear().rows.add(result).columns.adjust().draw();
            return tableInstance;
        }

        tableInstance = new DataTable(tableEl, {
            fixedColumns: true,
            colReorder: true,
            responsive: true,
            autoWidth: true,
            destroy: true,
            ordering: false,
            order: [[4, "desc"]],
            language: {
                lengthMenu: "แสดง _MENU_ แถว/หน้า",
                zeroRecords: "ไม่พบข้อมูล",
                info: "แสดงหน้า _PAGE_ ของ _PAGES_",
                infoEmpty: "ไม่มีรายการแสดง",
                search: "ค้นหา",
                paginate: {
                    first: "หน้าแรก",
                    previous: "ก่อนหน้า",
                    next: "ถัดไป",
                    last: "หน้าสุดท้าย"
                }
            },
            data: result,
            columns: [
                {
                    data: "car_brand",
                    className: "dt-body-center",
                    responsivePriority: 3,
                    render: brand =>
                        `<img src='/image/carbrand/${brand}.png' width='50'/>`
                },
                {
                    data: "car_model",
                    className: "text-wrap"
                },
                {
                    data: "title_masterplan",
                    className: "text-wrap"
                },
                {
                    data: "total_premiums",
                    responsivePriority: 2,
                    className: "text-nowrap dt-body-right",
                    render: (_data, _type, row) => {
                        const vol = Utility.formatToDisplayCurrency(parseFloat(row.total_premiums));
                        const com = Utility.formatToDisplayCurrency(parseFloat(row.cmi_total_premiums));
                        const total = row.buy_cmi?.toUpperCase() === "Y"
                            ? Utility.formatToDisplayCurrency(parseFloat(row.total_premiums) + parseFloat(row.cmi_total_premiums))
                            : vol;
                        return `<span>${total} บาท</span>
                        <br/><span class="text-black-50">สมัครใจ (${vol})</span>` +
                            (row.buy_cmi?.toUpperCase() === "Y"
                                ? `<br/><span class="text-black-50">พ.ร.บ. (${com})</span>` : "");
                    }
                },
                {
                    targets: 4,
                    data: "date_create",
                    visible: false,
                    searchable: false,
                    className: "dt-body-right",
                    render: date => moment(date).format("L"),
                    type: "date"
                },
                {
                    data: "date_create",
                    className: "dt-body-right",
                    render: function (data) {
                        const m = moment(data);
                        const thaiYear = m.year() + 543;
                        return m.format(`DD/MM/${thaiYear}`);
                    }
                },
                {
                    data: null,
                    className: "text-nowrap dt-body-center",
                    targets: [-1],
                    responsivePriority: 1,
                    render: row => row.is_notified === "Y"
                        ? `<button type="button" class="btn btn-sm btn-outline-secondary text-nowrap" title="แจ้งงาน" disabled><i class="fa-solid fa-ban"></i> แจ้งงานแล้ว</button>`
                        : `<button type="button" class="btn btn-sm btn-outline-success text-nowrap" title="แจ้งงาน"><i class="fa-solid fa-download"></i> แจ้งงาน</button>`
                }
            ],
            rowGroup: {
                className: "text-wrap",
                dataSrc: "quo_number",
                startRender: function (rows, group) {
                    const collapsed = collapsedGroups[group] === true;
                    const r = rows.data()[0];
                    const rowGroupEl = document.createElement("tr");

                    rowGroupEl.dataset.name = group;
                    rowGroupEl.className = collapsed ? "collapsed" : "";
                    rowGroupEl.innerHTML = `<td class="align-middle table-secondary" colspan="7">
                    <button type="button" class="btn btn-sm btn-outline-success opacity-100" disabled>
                        <b>เลขใบเสนอราคา:</b> ${group}
                        <b> ชื่อ-สกุล:</b> ${r.name} ${r.last_name}
                        <b> เลขทะเบียนรถ:</b> ${r.vehicle_license}
                        <span class="badge text-bg-danger">${rows.count()}</span>
                    </button>
                </td>`;
                    return rowGroupEl;
                }
            }
        });

        tableInstance.clear().rows.add(result).columns.adjust().draw();

        return tableInstance;
    } catch (err) {
        console.error("โหลดข้อมูล", err);
    }
}

// โหลดข้อมูลคำนำหน้า
export async function loadPrenameOptions(flagType = "N") {
    try {
        const result = await ApiCores.fetchPrename({ INS_COMPANY_CODE: "TNI" });
        const customerPrefixSelected = document.getElementById("preNameTitleSelect");
        const beneficiaryPrefixSelected = document.getElementById("beneficiaryPrefixSelect");
        const highlightCodes = ["007", "001", "002", "003", "935", "196", "194", "929", "152"];

        const highlightOptions = result.data
            .filter(v => highlightCodes.includes(v.PRENAME_CODE))
            .map(v => `<option class="bg-warning bg-opacity-25" value="${v.PRENAME_CODE}" gender="${v.FLAG_SEX || ""}">${v.PRENAME_CODE_TNI}</option>`);

        const normalOptions = result.data
            .filter(v => !highlightCodes.includes(v.PRENAME_CODE) && flagType === "N" ? v.FLAG_TYPE !== "C" : v.FLAG_TYPE === "C")
            .map(v => `<option value="${v.PRENAME_CODE}" gender="${v.FLAG_SEX || ""}">${v.PRENAME_CODE_TNI}</option>`);

        customerPrefixSelected.innerHTML = `<option value="">กรุณาเลือก</option>` + highlightOptions.join("") + normalOptions.join("");

        const normalOptions1 = result.data
            .filter(v => !highlightCodes.includes(v.PRENAME_CODE))
            .map(v => `<option value="${v.PRENAME_CODE}" gender="${v.FLAG_SEX || ""}">${v.PRENAME_CODE_TNI}</option>`);

        beneficiaryPrefixSelected.innerHTML = `<option value="">กรุณาเลือก</option>` + highlightOptions.join("") + normalOptions1.join("");
    } catch (err) {
        console.error("โหลดข้อมูลคำนำหน้าไม่สำเร็จ", err);
    }
}

// โหลดข้อมูลจังหวัดจดทะเบียนรถ
async function loadCarRegisterProvinceOptions() {
    try {
        const result = await ApiCores.fetchProvince();
        const select = document.getElementById("registrationCarProvinceSelect");
        select.innerHTML = `<option value="">กรุณาเลือก</option>` + result.data
            .map(v => `<option value="${v.PROVINCE_CODE}" >${v.PROV_NAME_T}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลจังหวัดจดทะเบียนไม่สำเร็จ", err);
    }
}

// โหลดข้อมูลอาชีพ
async function loadOccupationOptions() {
    try {
        const result = await ApiCores.fetchOccupation({ INS_COMPANY_CODE: "TNI" });
        const select = document.getElementById("customerOccupationSelect");

        select.innerHTML = `<option value="">กรุณาเลือก</option>` + result.data
            .map(v => `<option value="${v.OCC_CODE}" >${v.OCC_CODE_TNI}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลอาชีพไม่สำเร็จ", err);
    }
}

// โหลดที่อยู่สำหรับส่งเอกสารตามลูกค้า
export async function loadSendDocAddressByCustomer() {
    const addressNoInput = document.getElementById("customerAddressNoInput");
    const provinceSelect = document.getElementById("customerAddressProvinceSelect");
    const districtSelect = document.getElementById("customerAddressDistrictSelect");
    const subdistrictSelect = document.getElementById("customerAddressSubDistrictSelect");
    const zipcodeInput = document.getElementById("customerAddressZipCodeInput");

    const addressNoAgentInput = document.getElementById("sendAddressNoInput");
    const provinceAgentSelect = document.getElementById("sendAddressProvinceSelect");
    const districtAgentSelect = document.getElementById("sendAddressDistrictSelect");
    const subdistrictAgentSelect = document.getElementById("sendAddressSubDistrictSelect");
    const zipcodeAgentInput = document.getElementById("sendAddressZipCodeInput");

    addressNoAgentInput.value = addressNoInput.value;

    const provinceRes = await ApiCores.fetchProvince();
    provinceAgentSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
        provinceRes.data.map(v => `<option value="${v.PROVINCE_CODE}" ${v.PROVINCE_CODE === provinceSelect.value ? "selected" : ""} >${v.PROV_NAME_T}</option>`).join("");

    await clearSelect(districtAgentSelect, "กรุณาเลือก");
    await clearSelect(subdistrictAgentSelect, "กรุณาเลือก");
    zipcodeAgentInput.value = ""; // เคลียร์รหัสไปรษณีย์

    districtAgentSelect.innerHTML = `<option value="">กรุณาเลือก</option>`;
    if (provinceSelect.value) {
        const districtRes = await ApiCores.fetchDistrict({ "PROVINCE_CODE": provinceSelect.value });
        districtAgentSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
            districtRes.data.map(v => `<option value="${v.DISTRICT_CODE}" ${v.DISTRICT_CODE === districtSelect.value ? "selected" : ""} >${v.DISTRICT_NAME_T}</option>`).join("");
    }

    if (districtSelect.value) {
        const subdistrictRes = await ApiCores.fetchSubDistrict({ "PROVINCE_CODE": provinceSelect.value, "DISTRICT_CODE": districtSelect.value });
        subdistrictAgentSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
            subdistrictRes.data.map(v => `<option value="${v.I_SUBDISTRICT}" ${v.I_SUBDISTRICT === subdistrictSelect.value ? "selected" : ""} zipcode="${v.I_ZIPCODE}">${v.SUBDISTRICT}</option>`).join("");

        zipcodeAgentInput.value = subdistrictAgentSelect.selectedOptions[0].getAttribute("zipcode") ?? zipcodeInput.value;
    }
}

// โหลดที่อยู่สำหรับส่งเอกสารตามตัวแทน
export async function loadSendDocAddressByAgent() {
    try {
        const result = await ApiCores.fetchAgentDetails();
        const sendAddressInput = document.getElementById("sendAddressNoInput");
        sendAddressInput.value = `${result.data[0].F_ADDRESS_NO} ${result.data[0].F_SOI} ${result.data[0].F_STREET}`;

        const provinceSelect = document.getElementById("sendAddressProvinceSelect");
        const districtSelect = document.getElementById("sendAddressDistrictSelect");
        const subdistrictSelect = document.getElementById("sendAddressSubDistrictSelect");
        const inputZipcode = document.getElementById("sendAddressZipCodeInput");

        const provinceRes = await ApiCores.fetchProvince();
        provinceSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
            provinceRes.data.map(v => `<option value="${v.PROVINCE_CODE}" ${v.PROVINCE_CODE === result.data[0].F_PROVINCE_CODE ? "selected" : ""} >${v.PROV_NAME_T}</option>`).join("");

        if (provinceSelect.value) {
            const districtRes = await ApiCores.fetchDistrict({ "PROVINCE_CODE": provinceSelect.value });
            districtSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
                districtRes.data.map(v => `<option value="${v.DISTRICT_CODE}" ${v.DISTRICT_NAME_T === result.data[0].DISTRICT_NAME_T ? "selected" : ""} >${v.DISTRICT_NAME_T}</option>`).join("");
        }

        if (provinceSelect.value && districtSelect.value) {
            const subdistrictRes = await ApiCores.fetchSubDistrict({ "PROVINCE_CODE": provinceSelect.value, "DISTRICT_CODE": districtSelect.value });
            subdistrictSelect.innerHTML = `<option value="">กรุณาเลือก</option>` +
                subdistrictRes.data.map(v => `<option value="${v.I_SUBDISTRICT}" ${v.SUBDISTRICT === result.data[0].F_SUBDISTRICT ? "selected" : ""} zipcode="${v.I_ZIPCODE}">${v.SUBDISTRICT}</option>`).join("");

            inputZipcode.value = subdistrictSelect.selectedOptions[0].getAttribute("zipcode") ?? result.data[0].F_ZIPCODE;
        }
    } catch (err) {
        console.error("โหลดข้อมูลตัวแทนไม่สำเร็จ", err);
    }
}

// ล้าง select element และเพิ่ม option ค่าเริ่มต้น
export async function clearSelect(selectElement, defaultText = "กรุณาเลือก") {
    selectElement.innerHTML = "";
    const defaultOption = document.createElement("option");
    defaultOption.value = "";
    defaultOption.textContent = defaultText;
    selectElement.appendChild(defaultOption);
}

// โหลดข้อมูลยี่ห้อรถ
async function loadCarBrandOptions() {
    try {
        const result = await ApiCores.fetchCarBrand({ INS_COMPANY_CODE: "TNI" });
        const select = document.getElementById("carBrandSelect");

        select.innerHTML = `<option value=""></option>` + result.data
            .map(v => `<option value="${v.CARBRAND_CODE}" >${v.CARBRAND_NAME}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลยี่ห้อรถไม่สำเร็จ", err);
    }
}

// โหลดข้อมูลรุ่นรถตามยี่ห้อ
async function loadCarModelOptions(data) {
    try {
        const result = await ApiCores.fetchCarModel({ CARBRAND_CODE: data, INS_COMPANY_CODE: "TNI" });
        const select = document.getElementById("carModelSelect");

        select.innerHTML = `<option value=""></option>` + result.data
            .map(v => `<option value="${v.CARMODEL_CODE}" >${v.CARMODEL_NAME}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลรุ่นรถไม่สำเร็จ", err);
    }
}

// โหลดข้อมูลรหัสรถยนต์ตามประเภท
async function loadCarVoluntaryCode(carType) {
    try {
        const result = await ApiCores.fetchCarVoluntaryCode();
        const select = document.getElementById("vehicleCodeSelect");

        select.innerHTML = `<option value="">กรุณาเลือก</option>` + result.data
            .filter(v => v.CAR_MODEL === carType) // กรองเฉพาะที่ไม่ใช่ "N"
            .map(v => `<option value="${v.CODECAR}" seat="${v.CAR_SEAT}" cmi="${v.SUBINS_TYPE_CODE}" >${v.CODECAR} ${v.CARUSED_DESC}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลรุ่นรถไม่สำเร็จ", err);
    }
}

// โหลดข้อมูลสีรถ
async function loadCarColorOptions() {
    try {
        const result = await ApiCores.fetchCarColor({ INS_COMPANY_CODE: "TNI" });
        const select = document.getElementById("carColorSelect");

        select.innerHTML = `<option value="">กรุณาเลือก</option>` + result.data
            .map(v => `<option value="${v.COLOUR_CODE}" >${v.COLOUR_NAME}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลสีรถไม่สำเร็จ", err);
    }
}

// ฟังก์ชันสำหรับจัดการการเปลี่ยนแปลง radio ของลูกค้า
export function handleRadioCustomerChange() {
    const isPersonRadio = document.getElementById("personRadio").checked; // ตัวเลือกบุคคลธรรมดา
    const isCorporationRadio = document.getElementById("corporationRadio").checked; // ตัวเลือกนิติบุคคล
    const isCitizenIdRadio = document.getElementById("citizenIdRadio").checked; // ตัวเลือกบัตรประชาชน
    const isPassportIdRadio = document.getElementById("passportIdRadio").checked; // ตัวเลือกหนังสือเดินทาง
    const idCard = document.getElementById("citizenIdInput"); // input ของเลขบัตรประชาชน

    toggleSections(isCorporationRadio, isPersonRadio); // แสดง/ซ่อน section
    updateLabels(isCorporationRadio, isCitizenIdRadio); // แสดง/ซ่อน label

    // ถ้าเป็นบุคคลธรรมดา หรือนิติบุคคล ไม่ต้องตรวจสอบเลขบัตร
    if (isCorporationRadio || isPassportIdRadio) {
        setErrorMessage(""); // ซ่อน error
        return;
    }

    // ตรวจสอบความถูกต้องของเลขบัตรประชาชน
    const idValue = idCard.value.trim();
    if (isPersonRadio && idValue.length !== 13 && idValue !== "") {
        setErrorMessage("เลขบัตรผิด", true); // แสดงข้อความ error
        return;
    }

    // ตรวจสอบความถูกต้องของเลขบัตรประชาชน
    if (idValue !== "") {
        const idClean = idValue.replace(/-/g, ""); // เคลียร์เลขบัตร
        const isValid = Validations.isValidateThaiID(idClean); // ตรวจสอบความถูกต้อง
        setErrorMessage(isValid ? "เลขบัตรถูกต้อง" : "เลขบัตรผิด", !isValid); // แสดงข้อความ error
    } else {
        setErrorMessage(""); // ซ่อน error
    }

    const prefixSelect = document.getElementById("beneficiaryPrefixSelect"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const preNameTitleSelect = document.getElementById("preNameTitleSelect"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const nameInput = document.getElementById("beneficiaryNameInput"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const firstNameInput = document.getElementById("firstNameInput"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const lastNameInput = document.getElementById("lastNameInput"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const corporationNameInput = document.getElementById("corporationNameInput"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const selectedValue = document.querySelector('[name="customerTypeRadio"]:checked').value; // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย
    const byInsuredCheckBox = document.getElementById("byInsuredCheckBox"); // เช็คบ็อกซ์สำหรับการระบุผู้เอาประกันภัย

    // ถ้าเลือกให้ผู้เอาประกันภัยเป็นผู้รับผลประโยชน์
    if (byInsuredCheckBox.checked) {
        // ถ้าเลือกให้ผู้เอาประกันภัยเป็นผู้รับผลประโยชน์ → ปิดการแก้ไข
        prefixSelect.value = preNameTitleSelect.value; // ถ้าเลือกผู้รับผลประโยชน์ ให้ใช้ชื่อผู้รับผลประโยชน์
        nameInput.value = selectedValue === "N" ? `${firstNameInput.value} ${lastNameInput.value}` : `${corporationNameInput.value}`; // ถ้าเลือกผู้รับผลประโยชน์ ให้ใช้ชื่อผู้รับผลประโยชน์
    }
}

// ฟังก์ชันสำหรับจัดการการเปลี่ยนแปลง radio ของลูกค้า
function toggleSections(isCorp, isPerson) {
    document.getElementById("firstNameSection").hidden = isCorp; // ถ้าเป็นบุคคลธรรมดา
    document.getElementById("lastNameSection").hidden = isCorp; // ถ้าเป็นบุคคลธรรมดา
    document.getElementById("corporationSection").hidden = isPerson; // ถ้าเป็นนิติบุคคล
    document.getElementById("birthDateSection").hidden = isCorp; // ถ้าเป็นบุคคลธรรมดา
    document.getElementById("registerDateSection").hidden = isPerson; // ถ้าเป็นนิติบุคคล
    document.getElementById("customerTypeCardSection").hidden = isCorp; // ถ้าเป็นบุคคลธรรมดา

    // ถ้าเป็นบุคคลธรรมดา
    const errorSpan = document.querySelector("span.messageErrorCardIDSpan");
    if (errorSpan) errorSpan.style.display = isCorp ? "none" : "inline"; // ถ้าเป็นบุคคลธรรมดา
}

// ฟังก์ชันสำหรับอัปเดต label และ placeholder ของเลขบัตรประชาชน
function updateLabels(isCorp, isCitizen) {
    let label;
    let copyLabel;
    switch (true) {
        case isCorp:
            label = "เลขประจำตัวผู้เสียภาษี";
            copyLabel = "เอกสารสำเนาประจำตัวผู้เสียภาษี";
            break;
        case isCitizen:
            label = "เลขบัตรประชาชน";
            copyLabel = "เอกสารสำเนาบัตรประชาชน";
            break;
        default:
            label = "เลขหนังสือเดินทาง (Passport)";
            copyLabel = "เอกสารสำเนาหนังสือเดินทาง (Passport)";
    }

    document.getElementById("customerIdLabel").innerText = label; // อัปเดต label
    document.getElementById("citizenIdInput").setAttribute("placeholder", label); // อัปเดต placeholder
    document.getElementById("usePasswordBySpan").innerText = label; // อัปเดต label
    document.getElementById("copyCardIDLabel").innerText = copyLabel; // อัปเดต label
}

// ฟังก์ชันสำหรับจัดการการเปลี่ยนแปลง input ของเลขบัตรประชาชน
export function handleInputCustomerChange(e) {
    const isPersonRadio = document.getElementById("personRadio"); // ตัวเลือกบุคคลธรรมดา
    const isCorporationRadio = document.getElementById("corporationRadio"); // ตัวเลือกนิติบุคคล
    const isCitizenIdRadio = document.getElementById("citizenIdRadio"); // ตัวเลือกบัตรประชาชน

    // ถ้าเป็นบุคคลธรรมดา
    if ((isCorporationRadio.checked || isPersonRadio.checked) && isCitizenIdRadio.checked) {
        const char = String.fromCharCode(e.which || e.keyCode);
        // กรองเฉพาะตัวเลข
        if (/\D/.test(char)) {
            e.preventDefault();
        }
    }
    handleRadioCustomerChange(); // เรียกใช้ฟังก์ชัน
}

// ฟังก์ชันสำหรับตั้งค่าข้อความ error
function setErrorMessage(message, isError = true) {
    const errorSpan = document.querySelector("span.messageErrorCardIDSpan"); // ตัวแปรสําหรับเก็บ span
    if (!errorSpan) return; // ถ้าไม่มี span

    if (!message) {
        errorSpan.textContent = ""; // ล้างข้อความ
        errorSpan.style.display = "none"; // ซ่อน span
        errorSpan.className = "messageErrorCardIDSpan"; // reset class
        return;
    }

    errorSpan.textContent = message; // ตั้งค่าข้อความ
    errorSpan.style.display = "inline"; // แสดง span
    errorSpan.classList.remove("text-success", "text-danger"); // ล้าง class

    if (isError) {
        errorSpan.classList.add("text-danger"); // เพิ่ม class
        errorSpan.innerHTML += ' <i class="fa-solid fa-xmark text-danger"></i>'; // แทรก icon
    } else {
        errorSpan.classList.add("text-success"); // เพิ่ม class
        errorSpan.innerHTML += ' <i class="fa-solid fa-check text-success"></i>'; // แทรก icon
    }
}

// ฟังก์ชันสำหรับเปลี่ยนแปลงโหมดการตรวจรถ
export function toggleInspectionMode({ collapseAction, showMobile, showAgent, showWounds, disableRadioId }) {
    const disableRadio = document.getElementById(disableRadioId);
    const inspectionCollapse = new bootstrap.Collapse(document.getElementById('inspectionCollapse'), { toggle: false });
    disableRadio.setAttribute("disabled", true);

    if (collapseAction === 'toggle') {
        inspectionCollapse.toggle();
    } else {
        inspectionCollapse.hide();
    }

    document.getElementById('inspectionMobileSection').hidden = !showMobile;
    document.getElementById('inspectionAgentSection').hidden = !showAgent;
    document.getElementById('inspectionCarWoundsSection').hidden = !showWounds;

    updateCarInspectionSummary();

    setTimeout(() => {
        disableRadio.removeAttribute("disabled");
    }, 500);
}

// ฟังก์ชันสำหรับอัปเดตสรุปการตรวจรถ
export function updateCarInspectionSummary() {
    const selectedRadio = document.querySelector("input[name='resultsInspectionRadio']:checked");
    const resultText = selectedRadio ? selectedRadio.nextElementSibling.textContent : "";

    const agentSelect = document.getElementById("inspectionAgentSelect");
    const agentText = agentSelect.value === "A" ? "" : agentSelect.options[agentSelect.selectedIndex].text;

    const wounds = document.getElementById("inspectionCarWoundsInput").value;
    const woundsText = wounds ? `จำนวน ${wounds} แผล` : "";

    const isAgent = document.getElementById("agentInspectionRadio").checked;
    const agentSummary = isAgent ? ` ${agentText} ${woundsText}` : "";

    const isInsure = document.getElementById("insuranceInspectionRadio").checked;
    const mobile = document.getElementById("inspectionMobileInput").value;
    const mobileText = isInsure ? ` เบอร์ติดต่อ ${mobile}` : "";

    const summaryLabel = document.getElementById("inspectionRemarkLabel");
    summaryLabel.style.transition = "opacity 0.25s";
    summaryLabel.style.opacity = 0;

    setTimeout(() => {
        summaryLabel.textContent = resultText + agentSummary + mobileText;
        summaryLabel.style.opacity = 1;

         //ถ้าต้องการอัปเดต hidden input ด้วย:
         const remarks = document.getElementById("inspectionRemarkInput").value;
        document.getElementById("inspectionRemarkHidden").value = summaryLabel.textContent + " " + remarks;
    }, 250);
}

// ฟังก์ชันสำหรับจัดการการแสดงผลของ section การชำระเงิน
export function handleOnlinePaymentChannelChange() {
    const isQrCodeSelected = document.getElementById('qrCodeKBankRadio').checked;
    const isCreditCardSelected = document.getElementById('creditCardT2PRadio').checked;

    const netCommSection = document.getElementById('netCommPaymentSection');
    const fullPaymentSection = document.getElementById('fullPaymentSection');
    const netCommRadio = document.getElementById('netCommPaymentRadio');
    const fullPaymentRadio = document.getElementById('fullPaymentRadio');

    if (isQrCodeSelected) {
        netCommSection.hidden = false;
        fullPaymentSection.hidden = false;
        netCommRadio.checked = true;
        fullPaymentRadio.checked = false;
    } else if (isCreditCardSelected) {
        netCommSection.hidden = true;
        fullPaymentSection.hidden = false;
        netCommRadio.checked = false;
        fullPaymentRadio.checked = true;
    }
}

// ฟังก์ชันสำหรับจัดการการแสดงผลของ section การชำระเงิน
export function handlePaymentOnlineChannelChange() {
    const isPaymentOnlineSelected = document.getElementById('paymentOnlineRadio').checked;
    const isInstallmentOnlineSelected = document.getElementById('installmentOnlineRadio').checked;

    const installmentOnlineSection = document.getElementById('installmentOnlineBox');
    const paymentOnlineSection = document.getElementById('paymentOnlineBox');

    if (isPaymentOnlineSelected) {
        paymentOnlineSection.hidden = false;
        installmentOnlineSection.hidden = true;
    } else if (isInstallmentOnlineSelected) {
        paymentOnlineSection.hidden = true;
        installmentOnlineSection.hidden = false;
    }
}

// ฟังก์ชันสำหรับจัดการการอัปโหลดไฟล์
export async function handleFileUpload(element) {
    const file = element.files[0];
    const inputId = element.id;

    // แมป input ID กับ input text ที่ต้องใส่ค่าไฟล์
    const idMap = {
        copyCardIDFile: 'copyCardIDInput',
        copyRegistrationCarFile: 'copyRegistrationCarInput',
        copyDocumentFile: 'copyDocumentInput',
        inspectionCarFormFile: 'inspectionCarFormInput',
        inspectionCar1File: 'inspectionCar1Input',
        inspectionCar2File: 'inspectionCar2Input',
        inspectionCar3File: 'inspectionCar3Input',
        inspectionCar4File: 'inspectionCar4Input',
        inspectionCar5File: 'inspectionCar5Input',
        inspectionCar6File: 'inspectionCar6Input',
        inspectionCar7File: 'inspectionCar7Input',
        inspectionCar8File: 'inspectionCar8Input',
        inspectionCar9File: 'inspectionCar9Input',
        inspectionCar10File: 'inspectionCar10Input',
        inspectionCar11File: 'inspectionCar11Input',
        inspectionCar12File: 'inspectionCar12Input',
    };

    const targetInputId = idMap[inputId];
    if (!file) {
        // ไม่ได้เลือกไฟล์ → ล้างค่า
        if (targetInputId) {
            document.getElementById(targetInputId).value = "";
        }
        return;
    }

    // ตรวจสอบนามสกุล
    const isValidExt = await Validations.isValidateFileExtensionAsync(element);

    if (!isValidExt) {
        await Alert.showAlert(new Object({
            icon: `warning`,
            title: `<h4>แจ้งเตือน</h4>`,
            text: `<span class="text-danger">ประเภทไฟล์ไม่ถูกต้อง กรุณาอัปโหลดเฉพาะไฟล์ที่มีนามสกุล .jpg, .jpeg, .pdf, หรือ .png เท่านั้น</span>`,
        }));
        if (targetInputId) {
            document.getElementById(targetInputId).value = "";
        }
        return;
    }

    // ตรวจสอบขนาด
    const isValidSize = await Validations.isValidateFileSizeAsync(element);
    if (!isValidSize) {
        await Alert.showAlert(new Object({
            icon: `warning`,
            title: `<h4>แจ้งเตือน</h4>`,
            text: `<span class="text-danger">ขนาดไฟล์เกินกำหนด กรุณาอัปโหลดไฟล์ที่มีขนาดไม่เกิน 10 เมกะไบต์ (MB.)</span>`,
        }));
        if (targetInputId) {
            document.getElementById(targetInputId).value = "";
        }
        return;
    }

    // ตรวจสอบเนื้อหาไฟล์
    const isValidContent = await Validations.isValidateFileContentByRegex(element);
    if (!isValidContent) {
        await Alert.showAlert(new Object({
            icon: `warning`,
            title: `<h4>แจ้งเตือน</h4>`,
            text: `<span class="text-danger">พบเนื้อหาที่ไม่ปลอดภัยในไฟล์ กรุณาตรวจสอบและแก้ไขก่อนอัปโหลด</span>`,
        }));
        if (targetInputId) {
            document.getElementById(targetInputId).value = "";
        }
        return;
    }

    // ถ้าผ่านทั้งหมด ให้ set ค่า filename
    if (targetInputId) {
        document.getElementById(targetInputId).value = file.name;
    }
}

// โหลดข้อมูลใบเสนอราคาและแสดงในฟอร์ม
export async function loadData(dataQuotationDetail, dataTable) {
    document.getElementById("policyTypeInput").value = dataQuotationDetail.coverage_name; // ชื่อกรมธรรม์
    document.getElementById("productNameInput").value = dataQuotationDetail.title_masterplan; // ชื่อแผนประกัน
    document.getElementById("firstNameInput").value = dataTable.name; // ชื่อ
    document.getElementById("lastNameInput").value = dataTable.last_name; // นามสกุล
    document.getElementById("mobileCustomerInput").value = dataTable.phone_number; // เบอร์มือถือ

    const carBrandSelect = document.getElementById("carBrandSelect"); // เลือกยี่ห้อรถยนต์
    // เตรียม lookup ล่วงหน้า
    const textCarBrandMap = new Map(
        Array.from(carBrandSelect.options).map(option => [option.text.trim().toUpperCase(), option.value])
    );

    // ใช้ map ในการหาค่า
    const carBrandValue = textCarBrandMap.get(dataQuotationDetail.car_brand.toUpperCase()); // ค้นหาค่าจาก map

    if (carBrandValue) {
        carBrandSelect.value = carBrandValue;
        await loadCarModelOptions(carBrandValue); // โหลดรุ่นรถยนต์ตามยี่ห้อที่เลือก
        const carModelSelect = document.getElementById("carModelSelect"); // เลือกรุ่นรถยนต์

        // เตรียม lookup ล่วงหน้า
        const textCarModelMap = new Map(
            Array.from(carModelSelect.options).map(option => [option.text.trim().toUpperCase(), option.value])
        );

        // ใช้ map ในการหาค่า
        const carModelValue = textCarModelMap.get(dataQuotationDetail.car_model.toUpperCase()); // ค้นหาค่าจาก map
        carModelSelect.value = carModelValue || "";
    }

    // โหลดข้อมูลรหัสรถยนต์
    await loadCarVoluntaryCode((dataQuotationDetail.atm_type == "กระบะ 4 ประตู" ? "เก๋ง" : dataQuotationDetail.atm_type)); // ประเภทของรถยนต์

    const carResgistration = Utility.splitVehicleNumber(dataTable.vehicle_license); // แยกทะเบียนรถยนต์

    document.getElementById("registrationPreCarInput").value = carResgistration[0]; // หมวดอักษรทะเบียนรถยนต์
    document.getElementById("registrationCarInput").value = carResgistration[1]; // หมายเลขทะเบียนรถยนต์
    document.getElementById("registrationCarYearInput").value = dataTable.car_year; // ปีรถยนต์
    document.getElementById("vehicleSizeInput").value = dataQuotationDetail.car_engine_size; // ขนาดเครื่องยนต์

    document.getElementById("sumInsuranceInput").value = dataQuotationDetail.sum_insure === "" ? "-" : Utility.formatToDisplayCurrency(dataQuotationDetail.sum_insure, false); // จำนวนเงินเอาประกันภัย
    document.getElementById("deductInput").value = "-"; // ค่าเสียหายส่วนแรก
    document.getElementById("fireThiefInput").value = dataQuotationDetail.f_t === "" ? "-" : dataQuotationDetail.f_t; // ประกันภัยไฟไหม้และโจรกรรม

    document.getElementById("NetPremiumInput").value = Utility.formatToDisplayCurrency(dataQuotationDetail.net_premiums, false); // เบี้ยประกันภัยสุทธิ
    document.getElementById("TotalPremiumInput").value = Utility.formatToDisplayCurrency(dataQuotationDetail.total_premiums, false); // เบี้ยประกันภัยรวม
    document.getElementById("TotalCompulsoryInput").value = Utility.formatToDisplayCurrency(dataQuotationDetail.cmi_total_premiums); // เบี้ยประกันภัย พ.ร.บ.
    document.getElementById("SummaryInput").value = Utility.formatToDisplayCurrency(dataQuotationDetail.total_premiums_with_cmi); // เบี้ยประกันภัยรวม (รวม พ.ร.บ.)

    if (dataTable.buy_cmi === "Y") {
        document.getElementById("compulsoryRadio").checked = true;
        document.getElementById("compulsoryStartDateInput").classList.remove("readonly");
    } else {
        document.getElementById("nonCompulsoryRadio").checked = true;
        document.getElementById("compulsoryStartDateInput").classList.add("readonly");
    }

    if (dataQuotationDetail.repair_type === "S") {
        document.getElementById("showRoomRepairRadio").checked = true;
    } else {
        document.getElementById("garageRepairRadio").checked = true;
    }

    document.getElementById("quotationNoHidden").value = dataTable.quo_number; // เลขที่ใบเสนอราคา
    document.getElementById("quotationIdHidden").value = dataTable.id; // ID ใบเสนอราคา
    document.getElementById("premiumIdHidden").value = dataTable.premiums_id; // ID เบี้ยประกันภัย
    document.getElementById("productCodeHidden").value = dataTable.tm_product_code; // รหัสแผนประกัน
    document.getElementById("companyCodeHidden").value = dataTable.company_code; // รหัสบริษัท
    document.getElementById("coverTypeHidden").value = dataTable.coverage_code; // ประเภทความคุ้มครอง

}