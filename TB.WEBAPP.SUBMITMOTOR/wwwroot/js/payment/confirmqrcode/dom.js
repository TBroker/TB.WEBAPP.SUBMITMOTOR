import * as Events from "./event.js";

export async function initForm() {
    // ✅ โหลดข้อมูล + bind event


    // ✅ โหลด DOM ก่อน
    await Events.initSubmitConfrim();
}