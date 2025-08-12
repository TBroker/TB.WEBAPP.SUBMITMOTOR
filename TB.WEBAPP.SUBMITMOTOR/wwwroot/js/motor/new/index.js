//Entry point
import * as Dom from './dom.js';

// Importing the necessary modules for DOM manipulation and event handling
async function onDomReady() {
    await Dom.initForm(); // ✅ โหลดข้อมูล + bind event
}

// Wait for the DOM to be fully loaded before initializing
document.addEventListener("DOMContentLoaded", () => {
    onDomReady().catch(err => console.error("Init error:", err));
});