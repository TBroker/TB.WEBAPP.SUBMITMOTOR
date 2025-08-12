import * as Events from "./event.js";
import * as Validations from "../../helper/validations.js";
import * as Alert from "../../helper/alert.js";
import * as ApiCores from "../../service/apiCores.js";

export async function initForm() {
    // ✅ โหลดข้อมูล + bind event
    await loadRelationshipOptions();

    // ✅ โหลด DOM ก่อน
    await Events.initVerificationFile();
    await Events.initSubmitConfrim();
}

async function loadRelationshipOptions() {
    try {
        const result = await ApiCores.fetchRelationship();
        const select = document.getElementById("relationshipSelect");

        console.log(result);
        select.innerHTML = `<option value="">กรุณาเลือก</option>` + result.data
            .map(v => `<option value="${v.INS_RELATION_CODE}" >${v.INS_RELATIONSHIPS}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลอาชีพไม่สำเร็จ", err);
    }
}

export async function handleFileUpload(element) {
    const file = element.files[0];
    const inputId = element.id;

    const idMap = {
        documentFile: 'documentFileInput',
    };

    const targetInputId = idMap[inputId];

    // ถ้าไม่ได้เลือกไฟล์ → ล้างค่า
    if (!file) {
        if (targetInputId) {
            document.getElementById(targetInputId).value = "";
        }
        return;
    }

    // ตรวจสอบนามสกุล
    if (!await Validations.isValidateFileExtensionAsync(element)) {
        await Alert.showAlert({
            icon: "warning",
            title: "<h4>แจ้งเตือน</h4>",
            text: `<span class="text-danger">ประเภทไฟล์ไม่ถูกต้อง กรุณาอัปโหลดเฉพาะไฟล์ที่มีนามสกุล .jpg, .jpeg, .pdf, หรือ .png เท่านั้น</span>`,
        });
        return;
    }

    // ตรวจสอบขนาด
    if (!await Validations.isValidateFileSizeAsync(element)) {
        await Alert.showAlert({
            icon: "warning",
            title: "<h4>แจ้งเตือน</h4>",
            text: `<span class="text-danger">ขนาดไฟล์เกินกำหนด กรุณาอัปโหลดไฟล์ที่มีขนาดไม่เกิน 10 เมกะไบต์ (MB.)</span>`,
        });
        return;
    }

    // ตรวจสอบเนื้อหา
    if (!await Validations.isValidateFileContentByRegex(element)) {
        await Alert.showAlert({
            icon: "warning",
            title: "<h4>แจ้งเตือน</h4>",
            text: `<span class="text-danger">พบเนื้อหาที่ไม่ปลอดภัยในไฟล์ กรุณาตรวจสอบและแก้ไขก่อนอัปโหลด</span>`,
        });
        return;
    }

    // ถ้าผ่านทั้งหมด → เซ็ตชื่อไฟล์
    if (targetInputId) {
        document.getElementById(targetInputId).value = file.name;
    }
}
