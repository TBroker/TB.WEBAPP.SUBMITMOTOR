import * as Doms from './dom.js';

async function onDomReady() {
    await Doms.initRewardPoint(); // ✅ โหลดข้อมูล + bind event
}

document.addEventListener("DOMContentLoaded", () => {
    onDomReady().catch(err => console.error("Init error:", err));
});