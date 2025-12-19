import { http } from "./http";
import type { CreatePeopleDTO, PeopleSummaryDTO } from "~/domain/people/people.dto";
import type { People } from "~/domain/people/people.type";

export const PeopleApi = {
  list: () => http<PeopleSummaryDTO>("/user"),
  create: (dto: CreatePeopleDTO) =>
    http<People>("/user", { method: "POST", body: JSON.stringify(dto) }),
  remove: (id: string) =>
    http<void>(`/user/${id}`, { method: "DELETE" }),
};
