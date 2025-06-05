using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Vehicle
{
  public string RegistrationNumber { get; set; }
  public string Color { get; set; }
  public string Type { get; set; }
  public DateTime CheckInTime { get; set; }

  public Vehicle(string regNum, string color, string type)
  {
    RegistrationNumber = regNum;
    Color = color;
    Type = type;
    CheckInTime = DateTime.Now;
  }

  public bool IsOddPlate()
  {
    var match = Regex.Match(RegistrationNumber, @"\d+");
    if (match.Success && match.Value.Length > 0)
    {
      int lastDigit = int.Parse(match.Value.Last().ToString());
      return lastDigit % 2 != 0;
    }
    return false;
  }

  public bool IsEvenPlate()
  {
    var match = Regex.Match(RegistrationNumber, @"\d+");
    if (match.Success && match.Value.Length > 0)
    {
      int lastDigit = int.Parse(match.Value.Last().ToString());
      return lastDigit % 2 == 0;
    }
    return false;
  }
}

public class Slot
{
  public int SlotNumber { get; set; }
  public Vehicle Occupant { get; set; }
  public bool IsOccupied => Occupant != null;

  public Slot(int slotNumber)
  {
    SlotNumber = slotNumber;
    Occupant = null;
  }
}

public class ParkingLotSystem
{
  private Slot[] slots;
  private int totalSlots;

  public ParkingLotSystem()
  {
    totalSlots = 0;
    slots = new Slot[0];
  }

  public void CreateParkingLot(int size)
  {
    if (size <= 0)
    {
      Console.WriteLine("Ukuran parking lot harus lebih dari 0.");
      return;
    }

    totalSlots = size;
    slots = new Slot[totalSlots];
    for (int i = 0; i < totalSlots; i++)
    {
      slots[i] = new Slot(i + 1);
    }
    Console.WriteLine($"Created a parking lot with {totalSlots} slots");
  }

  public int Park(string regNum, string color, string type)
  {
    if (type != "Mobil" && type != "Motor")
    {
      Console.WriteLine("Sorry, only 'Mobil' and 'Motor' are allowed.");
      return -1;
    }

    for (int i = 0; i < totalSlots; i++)
    {
      if (!slots[i].IsOccupied)
      {
        Vehicle newVehicle = new Vehicle(regNum, color, type);
        slots[i].Occupant = newVehicle;
        Console.WriteLine($"Allocated slot number: {slots[i].SlotNumber}");
        return slots[i].SlotNumber;
      }
    }

    Console.WriteLine("Sorry, parking lot is full");
    return -1;
  }

  public bool Leave(int slotNumber)
  {
    if (slotNumber <= 0 || slotNumber > totalSlots)
    {
      Console.WriteLine("Invalid slot number.");
      return false;
    }

    int index = slotNumber - 1;

    if (slots[index].IsOccupied)
    {
      slots[index].Occupant = null;
      Console.WriteLine($"Slot number {slotNumber} is free");
      return true;
    }
    else
    {
      Console.WriteLine($"Slot number {slotNumber} is already empty.");
      return false;
    }
  }

  public void Status()
  {
    Console.WriteLine("Slot No.    Type        Registration No     Colour");
    foreach (var slot in slots.Where(s => s.IsOccupied).OrderBy(s => s.SlotNumber))
    {
      Console.WriteLine($"{slot.SlotNumber,-10} {slot.Occupant.Type,-10} {slot.Occupant.RegistrationNumber,-20} {slot.Occupant.Color,-10}");
    }
  }

  public int GetTypeOfVehiclesCount(string type)
  {
    return slots.Count(s => s.IsOccupied && s.Occupant.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
  }

  public List<string> GetRegistrationNumbersByPlateParity(bool isOdd)
  {
    return slots
        .Where(s => s.IsOccupied && (isOdd ? s.Occupant.IsOddPlate() : s.Occupant.IsEvenPlate()))
        .Select(s => s.Occupant.RegistrationNumber)
        .ToList();
  }

  public List<string> GetRegistrationNumbersByColor(string color)
  {
    return slots
        .Where(s => s.IsOccupied && s.Occupant.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
        .Select(s => s.Occupant.RegistrationNumber)
        .ToList();
  }

  public List<int> GetSlotNumbersByColor(string color)
  {
    return slots
        .Where(s => s.IsOccupied && s.Occupant.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
        .Select(s => s.SlotNumber)
        .ToList();
  }

  public int GetSlotNumberByRegistrationNumber(string regNum)
  {
    var slot = slots.FirstOrDefault(s => s.IsOccupied && s.Occupant.RegistrationNumber.Equals(regNum, StringComparison.OrdinalIgnoreCase));

    if (slot != null)
    {
      return slot.SlotNumber;
    }
    return -1;
  }

  public int GetOccupiedSlotsCount()
  {
    return slots.Count(s => s.IsOccupied);
  }

  public int GetAvailableSlotsCount()
  {
    return totalSlots - GetOccupiedSlotsCount();
  }
}

public class Program
{
  private static ParkingLotSystem parkingLot;

  public static void Main(string[] args)
  {
    parkingLot = new ParkingLotSystem();

    while (true)
    {
      Console.Write("$ ");
      string commandLine = Console.ReadLine();

      string[] parts = commandLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      if (parts.Length == 0)
      {
        continue;
      }

      string command = parts[0];

      try
      {
        switch (command)
        {
          case "create_parking_lot":
            if (parts.Length < 2)
            {
              Console.WriteLine("Usage: create_parking_lot <size>");
              break;
            }
            if (int.TryParse(parts[1], out int size))
            {
              parkingLot.CreateParkingLot(size);
            }
            else
            {
              Console.WriteLine("Invalid size for parking lot.");
            }
            break;

          case "park":
            if (parts.Length < 4)
            {
              Console.WriteLine("Usage: park <RegistrationNo> <Colour> <Type>");
              break;
            }
            string regNum = parts[1];
            string color = parts[2];
            string type = parts[3];
            parkingLot.Park(regNum, color, type);
            break;

          case "leave":
            if (parts.Length < 2)
            {
              Console.WriteLine("Usage: leave <SlotNumber>");
              break;
            }
            if (int.TryParse(parts[1], out int slotNum))
            {
              parkingLot.Leave(slotNum);
            }
            else
            {
              Console.WriteLine("Invalid slot number.");
            }
            break;

          case "status":
            parkingLot.Status();
            break;

          case "type_of_vehicles":
            if (parts.Length < 2)
            {
              Console.WriteLine("Usage: type_of_vehicles <Type>");
              break;
            }
            int count = parkingLot.GetTypeOfVehiclesCount(parts[1]);
            Console.WriteLine(count);
            break;

          case "registration_numbers_for_vehicles_with_ood_plate":
            var oddPlates = parkingLot.GetRegistrationNumbersByPlateParity(true);
            Console.WriteLine(string.Join(", ", oddPlates));
            break;

          case "registration_numbers_for_vehicles_with_event_plate":
            var evenPlates = parkingLot.GetRegistrationNumbersByPlateParity(false);
            Console.WriteLine(string.Join(", ", evenPlates));
            break;

          case "registration_numbers_for_vehicles_with_colour":
            if (parts.Length < 2)
            {
              Console.WriteLine("Usage: registration_numbers_for_vehicles_with_colour <Colour>");
              break;
            }
            var regNumsByColor = parkingLot.GetRegistrationNumbersByColor(parts[1]);
            Console.WriteLine(string.Join(", ", regNumsByColor));
            break;

          case "slot_numbers_for_vehicles_with_colour":
            if (parts.Length < 2)
            {
              Console.WriteLine("Usage: slot_numbers_for_vehicles_with_colour <Colour>");
              break;
            }
            var slotNumsByColor = parkingLot.GetSlotNumbersByColor(parts[1]);
            Console.WriteLine(string.Join(", ", slotNumsByColor));
            break;

          case "slot_number_for_registration_number":
            if (parts.Length < 2)
            {
              Console.WriteLine("Usage: slot_number_for_registration_number <RegistrationNo>");
              break;
            }
            int foundSlot = parkingLot.GetSlotNumberByRegistrationNumber(parts[1]);
            if (foundSlot != -1)
            {
              Console.WriteLine(foundSlot);
            }
            else
            {
              Console.WriteLine("Not found");
            }
            break;

          case "exit":
            return;

          case "occupied_slots_count":
            Console.WriteLine($"Jumlah lot terisi: {parkingLot.GetOccupiedSlotsCount()}");
            break;
          case "available_slots_count":
            Console.WriteLine($"Jumlah lot tersedia: {parkingLot.GetAvailableSlotsCount()}");
            break;

          default:
            Console.WriteLine("Unknown command.");
            break;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }
  }
}
