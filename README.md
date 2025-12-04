# 📡 Network Diagnostic Tool 2026

![C#](https://img.shields.io/badge/Language-C%23-239120)
![.NET](https://img.shields.io/badge/Framework-.NET_4.7.2-512BD4)
![Platform](https://img.shields.io/badge/Platform-Windows_Forms-0078D7)
![Status](https://img.shields.io/badge/Status-Prototype-orange)

A professional, **Windows Forms-based Network Diagnostic Tool** designed to assist System Administrators in troubleshooting local and remote network issues.

This project was developed as a practical application of **C#** and **.NET Framework** capabilities, featuring a custom **Modern Dark UI** built programmatically.

---

## 🚀 Key Features

### 1. 🌐 Asynchronous Ping
- Sends ICMP echo requests to any target host (Domain or IP).
- Uses `async/await` pattern to ensure the UI remains responsive during network operations.
- Reports roundtrip time and status.

### 2. 💻 System Information Scanner
- Retrieves the local **Hostname**.
- Scans network interfaces to find the **Local IPv4 Address** (filters out IPv6 automatically).
- Displays the current **Operating System** version.

### 3. 🗺️ Traceroute (Network Path Analysis)
- Traces the route packets take to reach a destination.
- Identifies hops, response times, and potential network bottlenecks.
- Implements a custom loop with increasing **TTL (Time-To-Live)** values.

### 4. 🎨 Custom Dark Mode UI
- Completely overrides standard Windows controls.
- Features a flat, modern design with a professional color palette (Dark Grey / Visual Studio Blue).
- Includes a terminal-style output display.

---

## 🛠️ Technologies Used

- **Language:** C#
- **Framework:** .NET Framework (Windows Forms)
- **Networking Libraries:** `System.Net`, `System.Net.NetworkInformation`, `System.Net.Sockets`
- **UI/UX:** Custom GDI+ Styling (No external libraries used)

---

## 📸 Screenshots

<img width="848" height="491" alt="image" src="https://github.com/user-attachments/assets/cc13d14d-bfde-433a-b99c-65da702c813f" />


---

## 👨‍💻 About the Developer

Developed by **Berkay H. Dural** as part of a portfolio to demonstrate competency in **Software Development** and **System Integration**.

[Visit My Portfolio](https://berkayhasip.com)
