import * as ApiCores from "../../service/apiCores.js";
import * as Events from "./event.js";

export async function initForm() {
    // ✅ โหลดข้อมูล + bind event
    await loadRelationshipOptions();

    // ✅ โหลด DOM ก่อน

    await Events.initSubmitConfrim();
}

async function loadRelationshipOptions() {
    try {
        const result = await ApiCores.fetchRelationship();
        const select = document.getElementById("relationshipSelect");
        select.innerHTML = `<option value="">กรุณาเลือก</option>` + result.data
            .map(v => `<option value="${v.INS_RELATION_CODE}" >${v.INS_RELATIONSHIPS}</option>`).join("");
    } catch (err) {
        console.error("โหลดข้อมูลอาชีพไม่สำเร็จ", err);
    }
}