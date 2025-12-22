# Weather Accuracy Monitor (VerifiClima)

**Weather Accuracy Monitor** is a platform designed to **collect, consolidate, and analyze the accuracy of weather forecasts** from multiple providers, comparing forecasts made at different moments with the actual observed data.

---

## 🧠 Architecture Overview

The solution is divided into three main parts:

### 1. Batch / ETL (AWS)
- Runs **once per day**
- Responsible for:
  - Fetching forecasts from multiple providers
  - Normalizing incoming data
  - Persisting historical forecast records
- Implemented as an **AWS Lambda (.NET 8)**
- Orchestrated using **EventBridge (cron)**
- Designed to evolve into a **Data Lake architecture**

### 2. Backend API
- REST API responsible for:
  - Exposing aggregated data to the frontend
  - Applying grouping and ordering rules (DayX, dates, sources)
- Built with **.NET 8**
- Domain-oriented architecture with clear layering
- Ready for horizontal scaling

### 3. Web Frontend
- Built with **Angular (modern syntax using @for / @if)**
- Dynamic table where:
  - Columns represent forecasted days
  - Rows represent when the forecast was made (DayX)
- Supports:
  - Light and dark themes
  - Responsive layout
  - Component-based forecast rendering per provider

---

## 🧱 Tech Stack

### Backend / Batch
- .NET 8  
    - Entity Framework
- AWS 
    - Lambda  
    - AWS EC2(planned)
    - DynamoDb(planned)
- PostgreSQL(Supabase)  


### Frontend
- Angular (standalone components)
- Angular Material
- Modern CSS (light/dark themes)
- Responsive design

### Infrastructure & DevOps
- Docker / Docker Compose
- GitHub Actions
- AWS SAM (local)

---

## 📊 Data Modeling (Concept)

- **DayX**: how many days before the real event the forecast was made  
- **DayCode**: forecasted date (YYYYMMDD)  
- **Sources**: multiple weather providers  

Comparison between:
- Forecasted min/max temperatures
- Real observed temperatures
- Weather descriptions

This model enables analysis such as:
- “Which provider is more accurate with 1, 3, or 7 days ahead?”
- “How accuracy degrades as the forecast horizon increases?”
- “Which services tend to overestimate or underestimate temperatures?”

---
