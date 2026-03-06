import { z } from "zod";
import { requiredString } from "../ultil/util";


export const activitySchema = z.object({
  title: requiredString("Title"),
  description: requiredString("Description"),
  category: requiredString("Category"),
  date: z.date({ error: "Date is required" }),
  location: z.object({
    venue: requiredString("Venue"),
    city: z.string({ error: "City" }).optional(),
    latitude: z.number({ error: "Lat" }),
    longitude: z.number({ error: "long" }),
  }),
});

export type ActivitySchema = z.infer<typeof activitySchema>;
