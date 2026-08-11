# CAHIER_DE_CHARGES — SteinsSwag

## Project summary

SteinsSwag is a two-part web application: an ASP.NET Core backend (API + EF Core persistence) and an Angular frontend. The goal is to deliver an MVP that helps buyers discover and contact sellers of rare fashion items, with secure seller management, item listings with images, and a minimal search/filter experience.

---

## Goals & success metrics

- Deliver an MVP where users can browse items, view item details, and contact sellers.
- Provide seller workflows to create/edit listings with images.
- Success metrics:
  - MVP functional end-to-end in staging within planned milestones.
  - Meaningful automated test coverage for service-layer logic.
  - API response time p95 < 300ms in a typical dev environment.
  - No plaintext secrets in repo; pipelines build and run tests automatically.

---

## Scope

### In-scope (MVP)
- Backend: Items, Sellers, Categories endpoints (CRUD), basic search and filtering, image upload, DB migrations, seed data.
- Frontend: Angular app pages: item list, item detail, create/edit item, seller page, contact form.
- Dev experience: local dev (dotnet & ng), CI for build/test, Docker for local compose.
- Auth: basic JWT-based seller authentication (register/login) for creating/editing items.

### Out-of-scope (initial)
- Payment processing, advanced analytics, marketplace transactions, user ratings, multi-tenant setups.

---

## Stakeholders & roles

- Product Owner (PO): defines features & prioritization.
- Tech Lead: architecture & technical review.
- Backend Developers: implement API, DB & services.
- Frontend Developers: implement Angular pages & integration.
- QA / Test Engineer: define test plans & run tests.
- DevOps Engineer: CI/CD, Docker, deployment.
- UX Designer (optional): UI/UX improvements.

---

## Deliverables

- CAHIER_DE_CHARGES.md (this file)
- API documentation (OpenAPI / Swagger)
- Postman/HTTP collection for endpoints
- Seed data & migration scripts
- Frontend dev server integration (env config)
- CI pipeline (GitHub Actions) to build & test both projects
- Dockerfiles + docker-compose for local dev
- Acceptance test cases & test results

---

## Project constraints & assumptions

- SQL Server is the production DB target (EF Core is used).
- Frontend dev server runs on http://localhost:4200; backend must allow CORS for that origin in dev.
- Secrets are injected via environment variables or secret stores; not committed to repo.
- Use existing tech choices: .NET 8+ (per project settings), Angular CLI v22 compatibility.

---

## Functional requirements (high-level)

1. Item listing:
   - GET /items -> paginated list, filter by category & status, sort (newest, price).
   - GET /items/{id} -> item details including images and seller info.
2. Item management (protected):
   - POST /items -> create listing (multipart with images).
   - PUT /items/{id} -> update listing.
   - DELETE /items/{id} -> delete listing.
3. Seller management:
   - Register, Login (JWT)
   - Endpoint GET /sellers/{id}/items
4. Contact seller:
   - POST /items/{id}/contact -> sends message to seller (email or store in DB).
5. Images:
   - Upload images and return accessible URLs. Local store in dev, cloud (Azure Blob/S3) in prod.
6. Search:
   - Basic keyword search across title & description.
7. Admin endpoints (optional initially):
   - Read-only admin endpoints for maintenance (health, migrations status).

---

## Non-functional requirements

- Security:
  - Use JWT for seller authentication.
  - Input validation on all API endpoints.
  - No secrets in repository.
- Performance:
  - API p95 < 300ms for read endpoints on small dataset.
- Reliability:
  - DB migrations are deterministic; CI checks migrations compile.
- Observability:
  - Structured logs (Serilog), health-check endpoint, and API error problem details.
- Maintainability:
  - Layered architecture: Api / Application / Domain / Infrastructure as already present.
- CI / CD:
  - PRs must pass CI pipeline before merge.

---

## Architecture overview

- SteinsSwag.Api (ASP.NET Core)
  - Controllers: API surface.
  - Program.cs: DI registration, middleware (exception handling, CORS).
  - Uses SteinsSwag.Application interfaces and SteinsSwag.Infrastructure DbContext.

- SteinsSwag.Application
  - DTOs, Interfaces, Services (business logic).
  - Service layer depends on repository patterns or DbContext abstractions.

- SteinsSwag.Domain
  - Entities (Item, Seller, Category), Enums (ItemStatus), Exceptions.

- SteinsSwag.Infrastructure
  - EF Core DbContext (SteinsSwagDbContext), Migrations, file storage for images (dev) or provider interface for blobs.

- steinsswag-client (Angular)
  - Components, services to call API, environment.ts to set API base URL.

---

## Data model & DB

- Example entities (confirm names from SteinsSwag.Domain/Entities):
  - Item: Id, Title, Description, Price, Status, CategoryId, SellerId, CreatedAt, UpdatedAt
  - Seller: Id, Name, Email, Bio, CreatedAt
  - Category: Id, Name
  - ItemImages: Id, ItemId, Url, IsPrimary
- Use migrations present in SteinsSwag.Infrastructure/Migrations.
- Provide seed data for dev: few sellers, categories, sample items with image URLs.

---

## API contract & versioning

- Use OpenAPI/Swagger in dev (already configured).
- Base path: /api/v1/
- Version with header or path when breaking changes occur. Provide a clear OpenAPI file and Postman collection.

---

## Security & auth

- JWT authentication for protected endpoints.
- Passwords hashed (e.g., PBKDF2/BCrypt).
- Use role claims (Seller, Admin) if needed.
- Rate-limit sensitive endpoints (contact, login) if exposed externally.

---

## CI/CD & deployment

- GitHub Actions to:
  - Build and test backend and frontend on PRs
  - Run security checks (dotnet restore with vulnerability scanner, npm audit)
  - Publish Docker images on main (optional)
- Docker:
  - API Dockerfile: build SDK image -> publish. Use environment variables for connection string.
  - Frontend Dockerfile: build Angular production bundle, serve with static server (nginx).
  - docker-compose.dev: SQL Server, API (mounted code), frontend (ng serve or built static).
- Deployment strategy:
  - Deploy to staging from main; manual promote to production.
  - Use migrations on startup only after a manual check or with a migration job.

---

## Development workflow & conventions

- Branching:
  - Use GitHub Flow:
    - main protected
    - feature/* branches for work
    - hotfix/* for urgent fixes
  - Each feature branch → pull request to main.
- Commits:
  - Conventional Commits: feat:, fix:, docs:, chore:, refactor:, test:, perf:
- Pull Requests:
  - Small PRs, 1-3 logical changes.
  - PR template with checklist (see below).
- Code review:
  - At least one approving reviewer + passing CI required to merge.
  - Review for correctness, readability, test coverage, and security concerns.
- Coding standards:
  - Backend: follow .editorconfig + Microsoft recommendations; format with dotnet format.
  - Frontend: Angular style guide; use linting (ESLint/Prettier).
- Dependency management:
  - Dependabot enabled; upgrade PRs reviewed and merged after passing CI.

---

## Quality assurance & testing

- Tests:
  - Unit tests: services, validators.
  - Integration tests: TestServer for controllers with in-memory or dockerized DB.
  - Frontend unit tests for components and services (Vitest/Jest).
  - E2E tests: Playwright or Cypress for critical flows (list → detail → contact).
- Coverage:
  - Aim for meaningful coverage for logic paths; coverage % target optional (e.g., 70%).
- QA process:
  - Create test plan for each feature.
  - Maintain acceptance tests that can be automated.

---

## Acceptance criteria & Definition of Done (DoD)

Definition of Done for a feature:
- Code compiles and passes static analysis & linters.
- Unit tests added; all new tests pass.
- Integration/E2E tests added or updated if behavior changed; CI green.
- Feature has API documentation (OpenAPI) reflecting endpoints.
- UI has basic UX and validation; fields tested.
- No secrets committed.
- PR reviewed and approved by at least one reviewer.
- Feature merged to main and deployed to staging.

Example feature acceptance (Item create):
- POST /api/v1/items accepts multipart/form-data + image files and returns 201 + location.
- Item stored with image URLs in DB and visible via GET /api/v1/items/{id}.
- Validation errors return ProblemDetails with clear messages (400).
- Unit tests cover service behavior; integration test exercises endpoint.

---

## Project milestones & timeline (example)

Sprint cadence: 2-week sprints.

- Sprint 0 (week 0): Setup & smoke tests
  - Task: Run both apps locally; make smoke tests work (CORS, DB).
  - Deliverable: CI pipeline scaffolded; Docker-compose for dev.

- Sprint 1 (weeks 1–2): Core API + DB
  - Task: Implement Items & Categories endpoints, DbContext checks, seed data.
  - Deliverable: API endpoints + migrations + Postman collection.

- Sprint 2 (weeks 3–4): Frontend listing & detail
  - Task: Implement Angular pages to list items & show details.
  - Deliverable: E2E test for browsing flow.

- Sprint 3 (weeks 5–6): Auth & seller flows
  - Task: JWT auth, seller register/login, protected item creation.
  - Deliverable: Protected endpoints + frontend seller pages.

- Sprint 4 (weeks 7–8): Image uploads & contact flow
  - Task: Implement multipart upload & seller contact flow.
  - Deliverable: Image upload working, items show images.

- Sprint 5: CI/CD, tests, docs & polish
  - Task: CI workflows, integration tests, README & CONTRIBUTING.md.
  - Deliverable: Merged CI workflows, deployable to staging.

---

## Risk register (high level)

- Secrets leaked in repo → mitigate by scanning & moving to env vars.
- Breakage from migrations → mitigate with dev/staging migrations testing.
- Image storage scaling → use provider abstraction and cloud storage for production.
- Auth complexity → keep MVP auth minimal and iteratively improve.

---

## Templates & checklists

### Feature spec template
- Title:
- Author:
- Summary:
- Motivation / business value:
- Scope:
- API changes (endpoints, verbs, payloads):
- DB changes (migrations, new tables/columns):
- Frontend changes (pages/components/services):
- Acceptance criteria (clear, testable):
- Security considerations:
- Rollout plan:
- Estimated effort (story points / hours):

### API contract template
- Endpoint: /api/v1/items
- Method: POST
- Auth: Bearer JWT (required)
- Request:
  - Content-Type: multipart/form-data
  - Fields: title (string, required), description (string), price (decimal), categoryId (int), images[] (file)
- Response:
  - 201 Created with Location header /api/v1/items/{id}
  - Body: ItemDto
- Errors:
  - 400 ProblemDetails for validation
  - 401 Unauthorized if not auth
  - 500 ProblemDetails for server errors

### Pull Request checklist (add to PR template)
- [ ] Linked issue
- [ ] Feature spec updated / referenced
- [ ] Tests added/updated
- [ ] Lint & formatting run
- [ ] CI green
- [ ] Security checks run (no secrets)
- [ ] Documentation updated (README, OpenAPI)

### PR review guide for reviewers
- Verify DoD items above; focus on API contract correctness, error handling, edge cases, and security issues.
- Run the feature locally if the change is significant; run integration tests if present.

---

## CI checklist

- Build backend: dotnet restore && dotnet build --no-restore
- Run backend tests: dotnet test
- Build frontend: npm ci && npm run build
- Run frontend tests: npm run test
- Optional: docker build for each Dockerfile to validate image builds

---

## Operational & maintenance notes

- Backups: schedule DB backups in production.
- Monitoring: add alerts for 5xx rates and high error logs.
- SLOs: p95 latency targets; uptime metrics.

---

## Project tracking (issues & labels)

- Use GitHub Issues with labels: epic, feature, bug, infra, chore, urgent, help wanted, blocked.
- Use milestones for sprints and releases.
- Maintain a project board for progress (To do / In progress / In review / Done).

---

## Appendix: repo-specific next steps to align with this cahier

1. Add this CAHIER_DE_CHARGES.md to repo root.
2. Add CONTRIBUTING.md with branch & PR rules + PR checklist.
3. Add a PR template with the checklist above.
4. Create GitHub Actions skeleton to build & test both projects on PR.
5. Add docker-compose.dev for local orchestration (SQL Server + API + frontend).
6. Inventory controllers, DTOs, and entities; produce an API spec (OpenAPI/POSTMAN).
7. Add seed data & a simple DevSeeder invoked conditionally in Program.cs when env is Development.

---

If you want I can also create a feature branch and open a PR instead of committing directly to the default branch — tell me the branch name if you prefer that workflow.