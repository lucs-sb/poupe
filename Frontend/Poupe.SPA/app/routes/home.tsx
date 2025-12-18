import type { Route } from "./+types/home";
import PeoplePage from "~/pages/people/people";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Poupe App" },
    { name: "description", content: "Controle de Gastos Residenciais" },
  ];
}

export default function Home() {
  return PeoplePage();
}
