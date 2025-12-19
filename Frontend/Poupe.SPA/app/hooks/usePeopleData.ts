import { useEffect, useState } from "react";
import type { People } from "~/domain/people/people.type";
import { PeopleApi } from "~/integration/people.api";

export function usePeopleData() {
  const [people, setPeople] = useState<People[]>([]);
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    if (!loaded) {
      PeopleApi.list().then((data) => {
        setPeople(data?.users ?? []);
        setLoaded(true);
      });
    }
  }, [loaded]);

  return { people, loaded };
}