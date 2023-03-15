import { environment } from "src/environments/environment";
import { UtilitiesService } from "../services/utilities.service";

export function jwtOptionsFactory(utilitiesService: UtilitiesService) {
  return {
    tokenGetter: () => {
      return utilitiesService.getToken();

    },
    authScheme: "Bearer ",
    allowedDomains: environment.backendDomain,
    disallowedRoutes: [], // not token in header
  }
}
