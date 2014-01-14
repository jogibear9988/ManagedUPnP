//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace ManagedUPnP.Extensions
{
    /// <summary>
    /// Provides extension methods to the IP Address class.
    /// </summary>
    internal static class IPAddressExtensions
    {
        #region Private Static Methods

        /// <summary>
        /// Returns the Network Address (Subnet) from an IPAddress and Subnet.
        /// </summary>
        /// <param name="address">The IP address to apply the subnet mask to.</param>
        /// <param name="subnetMask">The subnet mask to apply to the IP address.</param>
        private static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] lbIPAdressBytes = address.GetAddressBytes();
            byte[] lbSubnetMaskBytes = subnetMask.GetAddressBytes();

            if (lbIPAdressBytes.Length != lbSubnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] lbBroadcastAddress = new byte[lbIPAdressBytes.Length];
            
            for (int i = 0; i < lbBroadcastAddress.Length; i++)
                lbBroadcastAddress[i] = (byte)(lbIPAdressBytes[i] & (lbSubnetMaskBytes[i]));

            return new IPAddress(lbBroadcastAddress);
        }

        /// <summary>
        /// Gets an IP address of a certain size with a certain number of MSBs set for sub netting.
        /// </summary>
        /// <param name="totalBits">The total number of bits for the IP Address (must be multiple of 8).</param>
        /// <param name="setBits">The number of bits to set (must be less than or equal to Totalbits).</param>
        /// <returns>The created IPAddress.</returns>
        private static IPAddress IPAddressFromBitsSet(byte totalBits, byte setBits)
        {
            if (totalBits == 0)
                throw new ArgumentOutOfRangeException("totalBits", "must not be 0");

            return new IPAddress(ByteArrayFromBitsSet(totalBits, setBits));
        }

        /// <summary>
        /// Gets a set of bytes as an array with a total number of bits and certain number of MSB set.
        /// </summary>
        /// <param name="totalBits">The total number of bits for the bytes (must be multiple of 8).</param>
        /// <param name="setBits">The number of bits to set (must be less tahn or equal to TotalBits).</param>
        /// <returns>An array of bytes with the bits set.</returns>
        private static byte[] ByteArrayFromBitsSet(byte totalBits, byte setBits)
        {
            if (totalBits % 8 != 0)
                throw new ArgumentOutOfRangeException("totalBits", "must be a mutliple of 8");

            if (setBits > totalBits)
                throw new ArgumentOutOfRangeException("setBits", "must be less than or equal to totalBits");

            byte[] lbReturn = new Byte[((totalBits - 1) / 8) + 1];

            int liByte = 0;
            while (setBits > 0)
            {
                byte lbBits = (setBits >= 8 ? (byte)8 : setBits);
                lbReturn[liByte] = GetSetBits(lbBits);
                setBits -= lbBits;
                liByte++;
            }

            return lbReturn;
        }

        /// <summary>
        /// Gets a byte with a certain number of bits set.
        /// </summary>
        /// <param name="bits">The number of bits set (0 to 8).</param>
        /// <returns></returns>
        private static byte GetSetBits(byte bits)
        {
            if (bits > 8 || bits < 0)
                throw new ArgumentOutOfRangeException("bits", "must be between 0 and 8 (inclusive)");

            return (byte)(((1 << (bits + 1)) - 1) << (8 - bits));
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets whether an IP address is connectable from an interface described
        /// by unicast address info by checking the subnet ID. Compatible with IPv4 and
        /// IPv6 addresses. 
        /// </summary>
        /// <param name="address">The address to check for connectivity.</param>
        /// <param name="fromAddress">The unicase ip address information describing the interface.</param>
        /// <returns>True if on the same subnet, false otherwise.</returns>
        public static bool ConnectableFrom(this IPAddress address, UnicastIPAddressInformation fromAddress)
        {
            // If its an IPv4 address and its subnet matches the network subnet
            if (address.AddressFamily == AddressFamily.InterNetwork &&
                fromAddress.Address.AddressFamily == AddressFamily.InterNetwork &&
                address.SameSubnetAs(fromAddress.Address, fromAddress.IPv4Mask))
                return true;

            // If its an IPv6 address and both IP addresses are link local and they are on the same subnet
            if (address.AddressFamily == AddressFamily.InterNetworkV6 &&
                fromAddress.Address.AddressFamily == AddressFamily.InterNetworkV6 &&
                address.SameSubnetAs(fromAddress.Address))
                return true;

            return false;
        }

        /// <summary>
        /// Compares if 2 IPv6 addresses are within the same Subnet using the Subnet Mask
        /// </summary>
        /// <param name="address1">The first IPv4 address to compare the network IDs for.</param>
        /// <param name="address2">The second IPv4 address to compare the network IDs for.</param>
        /// <param name="subnetBits">The number of MSBs to use for comparing the subnet.</param>
        /// <returns>True if they are part of the same subnet</returns>
        public static bool SameSubnetAs(this IPAddress address1, IPAddress address2, byte subnetBits = 64)
        {
            if (address1.AddressFamily != AddressFamily.InterNetworkV6)
                throw new ArgumentException("must be an IPv6 (InterNetworkV6) address family.", "address1");

            if (address2.AddressFamily != AddressFamily.InterNetworkV6)
                throw new ArgumentException("must be an IPv6 (InterNetworV6) address family.", "address2");

            if (subnetBits > 128)
                throw new ArgumentException("must be between 0 and 128", "subnetBits");

            if (subnetBits != 0)
            {
                IPAddress lIPNet1 = address1.GetNetworkAddress(IPAddressFromBitsSet(128, subnetBits));
                IPAddress lIPNet2 = address2.GetNetworkAddress(IPAddressFromBitsSet(128, subnetBits));

                return lIPNet1.Equals(lIPNet2);
            }
            else
                return address1.Equals(address2);
        }

        /// <summary>
        /// Compares if 2 IPv4 addresses are within the same Subnet using the Subnet Mask
        /// </summary>
        /// <param name="address1">The first IPv4 address to compare the network IDs for.</param>
        /// <param name="address2">The second IPv4 address to compare the network IDs for.</param>
        /// <param name="subnetMask">The IPv4 subnet mask to apply to the addreses.</param>
        /// <returns>True if they are part of the same subnet</returns>
        public static bool SameSubnetAs(this IPAddress address1, IPAddress address2, IPAddress subnetMask)
        {
            if (address1.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException("must be an IPv4 (InterNetwork) address family.", "address1");

            if (address2.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException("must be an IPv4 (InterNetwork) address family.", "address2");

            if (subnetMask != null)
            {
                if (subnetMask.AddressFamily != AddressFamily.InterNetwork)
                    throw new ArgumentException("must be null or an IPv4 (InterNetwork) address family.", "subnetMask");

                IPAddress lIPNet1 = address1.GetNetworkAddress(subnetMask);
                IPAddress lIPNet2 = address2.GetNetworkAddress(subnetMask);

                return lIPNet1.Equals(lIPNet2);
            }
            else
                return address1.Equals(address2);
        }

        #endregion
    }
}
